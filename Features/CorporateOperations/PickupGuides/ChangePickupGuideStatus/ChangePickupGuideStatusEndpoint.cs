using Microsoft.EntityFrameworkCore;

public static class ChangePickupGuideStatusEndpoint
{
    private static readonly Dictionary<PickupGuideStatus, PickupGuideStatus[]> AllowedTransitions = new()
    {
        [PickupGuideStatus.Draft] = new[] { PickupGuideStatus.Collected, PickupGuideStatus.Cancelled },
        [PickupGuideStatus.Collected] = new[] { PickupGuideStatus.Processed, PickupGuideStatus.Cancelled },
        [PickupGuideStatus.Processed] = new[] { PickupGuideStatus.Invoiced, PickupGuideStatus.Cancelled },
        [PickupGuideStatus.Invoiced] = Array.Empty<PickupGuideStatus>(),
        [PickupGuideStatus.Cancelled] = Array.Empty<PickupGuideStatus>()
    };

    public static void MapChangePickupGuideStatus(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/status", async (
            long id,
            ChangePickupGuideStatusRequest request,
            RecepcionDbContext db,
            IPickupGuideReceiptPdfService pdfService,
            IFileStorageService storage,
            IEmailService emailService) =>
        {
            var guide = await db.PickupGuides.FirstOrDefaultAsync(x => x.Id == id);
            if (guide is null)
            {
                return Results.NotFound("Guía no encontrada.");
            }

            if (guide.Status == request.Status)
            {
                return Results.BadRequest(new[] { "La guía ya se encuentra en ese estado." });
            }

            if (!AllowedTransitions[guide.Status].Contains(request.Status))
            {
                return Results.BadRequest(new[] { $"No se puede cambiar el estado de '{guide.Status}' a '{request.Status}'." });
            }

            guide.Status = request.Status;
            guide.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            await db.SaveChangesAsync();

            // La constancia en PDF y el correo al cliente solo se generan al pasar a Collected;
            // un fallo aquí no revierte el cambio de estado, queda trazado en EmailDeliveryStatus/LastEmailError.
            if (request.Status == PickupGuideStatus.Collected)
            {
                await GenerateReceiptAndNotifyAsync(id, guide, db, pdfService, storage, emailService);
            }

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Estado de la guía actualizado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { guide.Id, guide.Status, guide.EmailDeliveryStatus }
            });
        })
        .WithName("ChangePickupGuideStatus")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task GenerateReceiptAndNotifyAsync(
        long guideId,
        PickupGuide guide,
        RecepcionDbContext db,
        IPickupGuideReceiptPdfService pdfService,
        IFileStorageService storage,
        IEmailService emailService)
    {
        var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        guide.LastEmailAttemptAt = now;

        byte[] pdfBytes;
        try
        {
            pdfBytes = await pdfService.GenerateAsync(guideId);

            var existingCurrentPdfs = await db.PickupGuideAttachments
                .Where(x => x.PickupGuideId == guideId && x.AttachmentType == PickupGuideAttachmentType.Pdf && x.IsCurrent)
                .ToListAsync();
            foreach (var old in existingCurrentPdfs)
            {
                old.IsCurrent = false;
            }

            var attachmentFileName = $"constancia-{guide.GuideNumber}.pdf";
            using var pdfStream = new MemoryStream(pdfBytes);
            var uploadResult = await storage.UploadAsync("Pdfs", guideId, attachmentFileName, "application/pdf", pdfStream);

            await db.PickupGuideAttachments.AddAsync(new PickupGuideAttachment
            {
                PickupGuideId = guideId,
                AttachmentType = PickupGuideAttachmentType.Pdf,
                OriginalFileName = attachmentFileName,
                StoredFileName = uploadResult.StoredFileName,
                FilePath = uploadResult.FilePath,
                ContentType = "application/pdf",
                FileSize = uploadResult.FileSize,
                IsCurrent = true,
                CreatedAt = now
            });
        }
        catch (Exception ex)
        {
            guide.EmailDeliveryStatus = PickupGuideEmailStatus.Failed;
            guide.LastEmailError = ex.Message.Length > 1000 ? ex.Message[..1000] : ex.Message;
            await db.SaveChangesAsync();
            return;
        }

        await PickupGuideEmailDispatcher.SendReceiptEmailAsync(
            guide, pdfBytes, $"constancia-{guide.GuideNumber}.pdf", "Se adjunta", db, emailService, now);

        await db.SaveChangesAsync();
    }
}
