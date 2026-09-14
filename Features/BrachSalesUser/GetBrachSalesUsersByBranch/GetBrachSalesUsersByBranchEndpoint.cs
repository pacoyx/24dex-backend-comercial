using Microsoft.EntityFrameworkCore;

public static class GetBrachSalesUsersByBranchEndpoint
{
    public static void MapGetBrachSalesUsersByBranch(this IEndpointRouteBuilder app)
    {
        app.MapGet("/byBranch/{branchId}", async (int branchId, RecepcionDbContext context) =>
        {
            var brachSalesUsers = await context.BrachSalesUsers.AsNoTracking()
            .Where(bsu => bsu.BranchSalesId == branchId)
            .Join(context.Users.AsNoTracking(),
                bsu => bsu.UserId,
                u => u.Id,
                (bsu, u) => new BrachSalesUserByBranchResponse(
                    bsu.Id,
                    bsu.BranchSalesId,
                    bsu.UserId,
                    u.Name,
                    bsu.Status
                ))
            .ToListAsync();

            var response = new ApiResponse<List<BrachSalesUserByBranchResponse>>
            {
                Data = brachSalesUsers,
                Message = "Lista de sucursales de usuario por sucursal",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
