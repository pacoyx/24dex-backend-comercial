using Microsoft.EntityFrameworkCore;

public static class GetSneakerDetailEndpoint
{
    public static void MapGetSneakerDetail(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/sneaker", async (int id, RecepcionDbContext db) =>
        {
            var sneakerDetail = await db.SneakerDetails
                .AsNoTracking()
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.WorkGuideDetailId == id);

            if (sneakerDetail is null)
                return Results.NotFound("No se encontró información de zapatilla para este detalle.");

            var response = new SneakerDetailResponse(
                sneakerDetail.Id, sneakerDetail.WorkGuideDetailId, sneakerDetail.SneakerBrandId,
                sneakerDetail.Brand?.Name, sneakerDetail.PrimaryColor, sneakerDetail.SecondaryColor,
                sneakerDetail.HasLaces, sneakerDetail.HasInsoles, sneakerDetail.Observations, sneakerDetail.CreatedAt);

            return Results.Ok(new ApiResponse<SneakerDetailResponse>
            {
                Success = true,
                Message = "Información de zapatilla encontrada.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
