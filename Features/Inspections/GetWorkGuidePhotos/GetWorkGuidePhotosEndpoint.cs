using Microsoft.EntityFrameworkCore;

public static class GetWorkGuidePhotosEndpoint
{
    public static void MapGetWorkGuidePhotos(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/photos", async (int id, RecepcionDbContext db) =>
        {
            var workGuideExists = await db.WorkGuideMains.AnyAsync(x => x.Id == id);
            if (!workGuideExists)
                return Results.NotFound("Guía de servicio no encontrada.");

            var photos = await db.ItemPhotos
                .AsNoTracking()
                .Where(p => p.ItemInspection.WorkGuideDetail.WorkGuideMainId == id)
                .OrderBy(p => p.CreatedAt)
                .Select(p => new WorkGuidePhotoResponse(
                    p.Id, p.ItemInspection.WorkGuideDetailId, p.ItemInspectionId, p.FileName, p.BlobUrl, p.CreatedAt))
                .ToListAsync();

            var response = new WorkGuidePhotosResponse(id, photos);

            return Results.Ok(new ApiResponse<WorkGuidePhotosResponse>
            {
                Success = true,
                Message = "Listado de fotografías de la guía de servicio.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
