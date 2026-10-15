using Microsoft.EntityFrameworkCore;

public static class DownloadItemPhotoEndpoint
{
    public static void MapDownloadItemPhoto(this IEndpointRouteBuilder app)
    {
        app.MapGet("/photos/{photoId:long}/download", async (
            long photoId,
            RecepcionDbContext db,
            IFileStorageService storage,
            CancellationToken cancellationToken) =>
        {
            var photo = await db.ItemPhotos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == photoId, cancellationToken);

            if (photo is null)
            {
                return Results.NotFound("Fotografía no encontrada.");
            }

            var stream = await storage.OpenReadAsync(photo.BlobUrl, cancellationToken);

            return Results.File(stream, photo.ContentType, enableRangeProcessing: true);
        })
        .WithName("DownloadItemPhoto")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
