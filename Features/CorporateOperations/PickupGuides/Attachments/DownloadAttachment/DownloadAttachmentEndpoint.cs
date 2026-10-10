using Microsoft.EntityFrameworkCore;

public static class DownloadAttachmentEndpoint
{
    public static void MapDownloadAttachment(this IEndpointRouteBuilder app)
    {
        app.MapGet("/attachments/{attachmentId:long}/download", async (
            long attachmentId,
            RecepcionDbContext db,
            IFileStorageService storage,
            CancellationToken cancellationToken) =>
        {
            var attachment = await db.PickupGuideAttachments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == attachmentId, cancellationToken);

            if (attachment is null)
            {
                return Results.NotFound("Evidencia no encontrada.");
            }

            var stream = await storage.OpenReadAsync(attachment.FilePath, cancellationToken);

            return Results.File(stream, attachment.ContentType, enableRangeProcessing: true);
        })
        .WithName("DownloadPickupGuideAttachment")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
