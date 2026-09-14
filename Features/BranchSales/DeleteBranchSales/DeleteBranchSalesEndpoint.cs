using Microsoft.EntityFrameworkCore;

public static class DeleteBranchSalesEndpoint
{
    public static void MapDeleteBranchSales(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var branchSale = await context.BranchSales
            .FirstOrDefaultAsync(bs => bs.Id == id);

            if (branchSale == null)
            {
                return Results.NotFound();
            }

            context.BranchSales.Remove(branchSale);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Sucursal eliminada",
                Message = "Sucursal eliminada",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
