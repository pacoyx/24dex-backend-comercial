public static class CreateBranchSalesEndpoint
{
    public static void MapCreateBranchSales(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateBranchSalesDto createBranchSalesDto, RecepcionDbContext context) =>
        {
            if (createBranchSalesDto.Description == null || createBranchSalesDto.Status == null){
                return Results.BadRequest("Description and Status are required");
            }

            var branchSale = new BranchSales
            {
                Description = createBranchSalesDto.Description,
                Address = createBranchSalesDto.Address,
                Status = createBranchSalesDto.Status
            };

            await context.BranchSales.AddAsync(branchSale);
            await context.SaveChangesAsync();
            return Results.Ok(branchSale);
        });
    }
}