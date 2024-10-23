using Microsoft.EntityFrameworkCore;

public static class GetCatServicesEndpoints
{
    public static void MapCatServices(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var catServices = await context.CatServices.AsNoTracking().ToListAsync();
            return Results.Ok(catServices);
        });
    }
}