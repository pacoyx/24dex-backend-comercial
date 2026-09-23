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
                                         .Join(context.CatServices,
                                             ps => ps.CatServiceId,
                                             cs => cs.Id,
                                             (ps, cs) => new
                                             {
                                                 ps.Id,
                                                 ps.Name,
                                                 ps.Price,
                                                 CategoryDescription = cs.Name
                                             })
                                         .ToListAsync();
             return Results.Ok(prodServices);
         });
    }
}