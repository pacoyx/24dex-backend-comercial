using DexterCompany.Models;
using Microsoft.EntityFrameworkCore;

public static class UpdateCatServiceEndpoint
{
    public static void MapUpdateCatService(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (RecepcionDbContext context, int id, CatService request) =>
        {
            if (id <= 0)
            {
                return Results.BadRequest("Id must be greater than zero");
            }

            var catService = await context.CatServices.FirstOrDefaultAsync(x => x.Id == id);
            if (catService == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(catService);
        });
    }
}