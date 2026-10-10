using Microsoft.EntityFrameworkCore;

public static class ResendPickupGuideEmailEndpoint
{
    public static void MapResendPickupGuideEmail(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/resend-email", async (
            long id,
            RecepcionDbContext db,
            IFileStorageService storage,
            IEmailService emailService) =>
        {
            var guide = await db.PickupGuides.FirstOrDefaultAsync(x => x.Id == id);
            if (guide is null)
            {
                return Results.NotFound("Guía no encontrada.");
            }

            var currentPdf = await db.PickupGuideAttachments
                .FirstOrDefaultAsync(x => x.PickupGuideId == id && x.AttachmentType == PickupGuideAttachmentType.Pdf && x.IsCurrent);

            if (currentPdf is null)
            {
                return Results.BadRequest(new[] { "La guía no tiene una constancia PDF generada todavía. Debe pasar primero a estado 'Collected'." });
            }

            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            guide.LastEmailAttemptAt = now;
            guide.EmailRetryCount += 1;

            byte[] pdfBytes;
            await using (var pdfStream = await storage.OpenReadAsync(currentPdf.FilePath))
            using (var memoryStream = new MemoryStream())
            {
                await pdfStream.CopyToAsync(memoryStream);
                pdfBytes = memoryStream.ToArray();
            }

            await PickupGuideEmailDispatcher.SendReceiptEmailAsync(
                guide, pdfBytes, currentPdf.OriginalFileName, "Se reenvía", db, emailService, now);

            await db.SaveChangesAsync();

            var success = guide.EmailDeliveryStatus == PickupGuideEmailStatus.Sent;

            return Results.Ok(new ApiResponse<object>
            {
                Success = success,
                Message = success ? "Constancia reenviada correctamente." : "No se pudo reenviar la constancia.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { guide.Id, guide.EmailDeliveryStatus, guide.EmailRetryCount, guide.LastEmailError }
            });
        })
        .WithName("ResendPickupGuideEmail")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
