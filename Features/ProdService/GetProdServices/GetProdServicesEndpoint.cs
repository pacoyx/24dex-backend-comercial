using Microsoft.EntityFrameworkCore;

public static class GetProdServicesEndpoint
{
    public static void MapGetProdServices(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var prodServices = await context.ProdServices.AsNoTracking().ToListAsync();
            return Results.Ok(prodServices);
        });
    }
}