using Microsoft.EntityFrameworkCore;

public static class CreateItemPhotoEndpoint
{
    public static void MapCreateItemPhoto(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:int}/photos", async (
            int id,
            IFormFile file,
            RecepcionDbContext db,
            IFileStorageService storage) =>
        {
            var workGuideExists = await db.WorkGuideMains.AnyAsync(x => x.Id == id);
            if (!workGuideExists)
                return Results.NotFound("Guía de servicio no encontrada.");

            if (file.Length == 0)
                return Results.BadRequest(new[] { "El archivo está vacío." });

            await using var stream = file.OpenReadStream();
            var uploadResult = await storage.UploadAsync("Inspections", "Photos", id, file.FileName, file.ContentType, stream);

            var photo = new ItemPhoto
            {
                WorkGuideMainId = id,
                FileName = uploadResult.StoredFileName,
                OriginalFileName = file.FileName,
                BlobUrl = uploadResult.FilePath,
                ContentType = file.ContentType,
                FileSize = uploadResult.FileSize,
                CreatedAt = DateTime.UtcNow
            };

            await db.ItemPhotos.AddAsync(photo);
            await db.SaveChangesAsync();

            var response = new ItemPhotoResponse(
                photo.Id, photo.WorkGuideMainId, photo.FileName, photo.OriginalFileName, photo.ContentType, photo.FileSize, photo.CreatedAt);

            return Results.Created($"/api/workguides/{id}/photos", new ApiResponse<ItemPhotoResponse>
            {
                Success = true,
                Message = "Fotografía registrada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = response
            });
        })
        .DisableAntiforgery()
        .WithName("CreateItemPhoto")
        .Produces<ApiResponse<ItemPhotoResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
