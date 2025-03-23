public static class GetProdServiceEndpoint
{
    public static RouteHandlerBuilder MapGetProdService(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/{id}", async (int id, RecepcionDbContext context) =>
        {
            if (id == 0)
            {
                return Results.BadRequest("Id is 0");
            }

            var prodService = await context.ProdServices.FindAsync(Convert.ToInt32(id));
            if (prodService == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(prodService);
        });
    }
}