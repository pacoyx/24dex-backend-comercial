using Microsoft.EntityFrameworkCore;

public static class GetCatServiceEndpoint
{
    public static void MapGetCatService(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (RecepcionDbContext context, int id) =>
        {
            if (id <= 0)
            {
                return Results.BadRequest("Id must be greater than zero");
            }

            var catService = await context.CatServices.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (catService == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(catService);
        });
    }
}