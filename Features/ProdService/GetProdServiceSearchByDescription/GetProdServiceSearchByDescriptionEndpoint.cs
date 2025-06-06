using Microsoft.EntityFrameworkCore;

public static class GetProdServiceSearchByDescriptionEndpoint
{
    public static RouteHandlerBuilder MapGetProdServiceSearchByDescription(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/search/{description}", async (string description, RecepcionDbContext context) =>
         {
             var prodServices = await context.ProdServices
                                         .AsNoTracking()
                                         .Where(ps => ps.Name.Contains(description))
                                         .OrderBy(ps => ps.Name)
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