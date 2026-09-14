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
                Status = createBranchSalesDto.Status,
                Code =  createBranchSalesDto.Code,
                CreatedAt = DateTime.Now,
                DefaultPrinterId =  createBranchSalesDto.DefaultPrinterId
            };

            await context.BranchSales.AddAsync(branchSale);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = branchSale.Id.ToString(),
                Message = "Sucursal creada",
                StatusCode = StatusCodes.Status201Created,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}