using Microsoft.EntityFrameworkCore;

public static class GetItemPhotosEndpoint
{
    public static void MapGetItemPhotos(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}/photos", async (long id, RecepcionDbContext db) =>
        {
            var inspectionExists = await db.ItemInspections.AnyAsync(x => x.Id == id);
            if (!inspectionExists)
                return Results.NotFound("Inspección no encontrada.");

            var photos = await db.ItemPhotos
                .AsNoTracking()
                .Where(x => x.ItemInspectionId == id)
                .OrderBy(x => x.CreatedAt)
                .Select(x => new ItemPhotoResponse(x.Id, x.ItemInspectionId, x.FileName, x.OriginalFileName, x.BlobUrl, x.CreatedAt))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<ItemPhotoResponse>>
            {
                Success = true,
                Message = "Listado de fotografías.",
                StatusCode = StatusCodes.Status200OK,
                Data = photos
            });
        });
    }
}
