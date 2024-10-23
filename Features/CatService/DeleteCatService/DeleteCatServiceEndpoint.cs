using Microsoft.EntityFrameworkCore;

public static class DeleteCatServiceEndpoint
{
    public static void MapDeleteCat(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
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

            context.CatServices.Remove(catService);
            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}