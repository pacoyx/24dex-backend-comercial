public static class DeleteProdServiceEndpoint
{
    public static void MapDeleteProdService(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var prodService = await context.ProdServices.FindAsync(id);
            if (prodService == null)
            {
                return Results.NotFound();
            }
            context.ProdServices.Remove(prodService);
            await context.SaveChangesAsync();
            return Results.Ok(prodService);
        });
    }
}