using Microsoft.EntityFrameworkCore;

public static class GetSneakerBrandsEndpoint
{
    public static void MapGetSneakerBrands(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var brands = await db.SneakerBrands
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new SneakerBrandResponse(x.Id, x.Name, x.IsActive))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<SneakerBrandResponse>>
            {
                Success = true,
                Message = "Listado de marcas de zapatillas.",
                StatusCode = StatusCodes.Status200OK,
                Data = brands
            });
        });
    }
}
