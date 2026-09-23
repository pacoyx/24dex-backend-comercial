using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateItemPhotoEndpoint
{
    public static void MapCreateItemPhoto(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/photos", async (
            long id,
            CreateItemPhotoRequest request,
            IValidator<CreateItemPhotoRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var inspectionExists = await db.ItemInspections.AnyAsync(x => x.Id == id);
            if (!inspectionExists)
                return Results.NotFound("Inspección no encontrada.");

            var photo = new ItemPhoto
            {
                ItemInspectionId = id,
                FileName = request.FileName,
                OriginalFileName = request.OriginalFileName,
                BlobUrl = request.BlobUrl,
                CreatedAt = DateTime.UtcNow
            };

            await db.ItemPhotos.AddAsync(photo);
            await db.SaveChangesAsync();

            var response = new ItemPhotoResponse(
                photo.Id, photo.ItemInspectionId, photo.FileName, photo.OriginalFileName, photo.BlobUrl, photo.CreatedAt);

            return Results.Created($"/api/inspections/{id}/photos", new ApiResponse<ItemPhotoResponse>
            {
                Success = true,
                Message = "Fotografía registrada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = response
            });
        })
        .RequireAuthorization();
    }
}
