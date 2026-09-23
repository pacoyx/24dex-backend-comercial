using Microsoft.EntityFrameworkCore;

public static class GetItemInspectionEndpoint
{
    public static void MapGetItemInspection(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/inspection", async (int id, RecepcionDbContext db) =>
        {
            var inspection = await db.ItemInspections
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.WorkGuideDetailId == id);

            if (inspection is null)
                return Results.NotFound("No se encontró inspección para este detalle.");

            var photoCount = await db.ItemPhotos.CountAsync(x => x.ItemInspectionId == inspection.Id);

            var response = new ItemInspectionResponse(
                inspection.Id, inspection.WorkGuideDetailId, inspection.HasStains, inspection.HasTears,
                inspection.HasFading, inspection.HasMissingParts, inspection.Notes, inspection.CreatedAt, photoCount);

            return Results.Ok(new ApiResponse<ItemInspectionResponse>
            {
                Success = true,
                Message = "Inspección encontrada.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
