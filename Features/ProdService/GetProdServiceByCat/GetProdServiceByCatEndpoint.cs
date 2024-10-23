using Microsoft.EntityFrameworkCore;

public static class GetProdServiceByCatEndpoint
{
    public static void MapGetProdServiceByCat(this IEndpointRouteBuilder app)
    {
        app.MapGet("/category/{category}", async (int category, RecepcionDbContext db) =>
        {
            var prodServices = await db.ProdServices
                                                .AsNoTracking()
                                                .Where(ps => ps.CatServiceId == category)
                                                .Select(ps => new
                                                {
                                                    ps.Id,
                                                    ps.Name,
                                                    ps.Price
                                                })
                                                .ToListAsync();
            return Results.Ok(prodServices);
        });
    }
}