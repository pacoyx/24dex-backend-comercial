using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class CreateAttachmentEndpoint
{
    private static readonly HashSet<PickupGuideAttachmentType> AllowedUploadTypes = new()
    {
        PickupGuideAttachmentType.Photo,
        PickupGuideAttachmentType.Signature
    };

    public static void MapCreateAttachment(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/attachments", async (
            long id,
            [FromForm] PickupGuideAttachmentType attachmentType,
            IFormFile file,
            RecepcionDbContext db,
            IFileStorageService storage) => await ExecuteAsync(id, attachmentType, file, db, storage))
        .DisableAntiforgery()
        .WithName("CreatePickupGuideAttachment")
        .Produces<ApiResponse<PickupGuideAttachmentResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }

    internal static async Task<IResult> ExecuteAsync(
        long id,
        PickupGuideAttachmentType attachmentType,
        IFormFile file,
        RecepcionDbContext db,
        IFileStorageService storage)
    {
            var guide = await db.PickupGuides.FirstOrDefaultAsync(x => x.Id == id);
            if (guide is null)
            {
                return Results.NotFound("Guía no encontrada.");
            }

            if (!AllowedUploadTypes.Contains(attachmentType))
            {
                return Results.BadRequest(new[] { "Solo se pueden subir evidencias de tipo Photo o Signature. El PDF se genera automáticamente al pasar la guía a Collected." });
            }

            if (file.Length == 0)
            {
                return Results.BadRequest(new[] { "El archivo está vacío." });
            }

            var category = attachmentType == PickupGuideAttachmentType.Photo ? "Photos" : "Signatures";

            await using var stream = file.OpenReadStream();
            var uploadResult = await storage.UploadAsync(category, id, file.FileName, file.ContentType, stream);

            var attachment = new PickupGuideAttachment
            {
                PickupGuideId = id,
                AttachmentType = attachmentType,
                OriginalFileName = file.FileName,
                StoredFileName = uploadResult.StoredFileName,
                FilePath = uploadResult.FilePath,
                ContentType = file.ContentType,
                FileSize = uploadResult.FileSize,
                IsCurrent = true,
                CreatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"))
            };

            await db.PickupGuideAttachments.AddAsync(attachment);
            await db.SaveChangesAsync();

            var response = new ApiResponse<PickupGuideAttachmentResponse>
            {
                Success = true,
                Message = "Evidencia registrada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = PickupGuideAttachmentsEndpoints.ToResponse(attachment)
            };

            return Results.Created($"/api/pickup-guides/{id}/attachments", response);
    }
}
