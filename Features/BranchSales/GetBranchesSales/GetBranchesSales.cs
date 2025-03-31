using Microsoft.EntityFrameworkCore;

public static class GetBranchesSales
{
    public static void MapGetBranchesSales(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var branchesSales = await context.BranchSales.AsNoTracking().ToListAsync();

            var response = new ApiResponse<List<BranchSalesComboResponse>>
            {
                Data = branchesSales.Select(bs => new BranchSalesComboResponse
                (
                    bs.Id,
                    bs.Description
                )).ToList(),
                Message = "Lista de sucursales de ventas",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}