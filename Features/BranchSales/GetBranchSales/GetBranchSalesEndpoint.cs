public static class GetBranchSalesEndpoint
{
    public static void MapGetBranchSales(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext context) =>
        {
            var branchSales = await context.BranchSales.FindAsync(Convert.ToInt32(id));
            if (branchSales == null){
                return Results.NotFound();
            }
            return Results.Ok(branchSales);
        });
    }
}