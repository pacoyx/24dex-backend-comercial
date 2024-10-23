using Microsoft.EntityFrameworkCore;

public static class GetBranchesSales
{
    public static void MapGetBranchesSales(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var branchesSales = await context.BranchSales.AsNoTracking().ToListAsync();
            return Results.Ok(branchesSales);
        });
    }
}