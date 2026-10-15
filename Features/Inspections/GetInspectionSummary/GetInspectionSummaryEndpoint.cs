using Microsoft.EntityFrameworkCore;

public static class GetInspectionSummaryEndpoint
{
    public static void MapGetInspectionSummary(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/inspection-summary", async (int id, RecepcionDbContext db) =>
        {
            var workGuide = await db.WorkGuideMains
                .AsNoTracking()
                .Include(x => x.WorkGuideDetails)
                    .ThenInclude(d => d.Product)
                .Include(x => x.WorkGuideDetails)
                    .ThenInclude(d => d.ItemInspection)
                .Include(x => x.WorkGuideDetails)
                    .ThenInclude(d => d.SneakerDetail)
                        .ThenInclude(s => s!.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (workGuide is null)
                return Results.NotFound("Guía de servicio no encontrada.");

            var details = workGuide.WorkGuideDetails.Select(d => new InspectionSummaryDetailResponse(
                d.Id,
                d.ProductId,
                d.Product?.Name ?? string.Empty,
                d.ItemInspection is null ? null : new ItemInspectionResponse(
                    d.ItemInspection.Id, d.ItemInspection.WorkGuideDetailId, d.ItemInspection.HasStains,
                    d.ItemInspection.HasTears, d.ItemInspection.HasFading, d.ItemInspection.HasMissingParts,
                    d.ItemInspection.Notes, d.ItemInspection.CreatedAt),
                d.SneakerDetail is null ? null : new InspectionSummarySneakerResponse(
                    d.SneakerDetail.Id, d.SneakerDetail.SneakerBrandId, d.SneakerDetail.Brand?.Name,
                    d.SneakerDetail.PrimaryColor, d.SneakerDetail.SecondaryColor, d.SneakerDetail.HasLaces,
                    d.SneakerDetail.HasInsoles, d.SneakerDetail.Observations)
            )).ToList();

            var response = new InspectionSummaryResponse(
                workGuide.Id, $"{workGuide.SerieGuia}-{workGuide.NumeroGuia}", details);

            return Results.Ok(new ApiResponse<InspectionSummaryResponse>
            {
                Success = true,
                Message = "Resumen de inspección obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
