using Microsoft.EntityFrameworkCore;

public static class GetBrachSalesUsersEndpoint
{
    public static void MapGetBrachSalesUsers(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var brachSalesUsers = await context.BrachSalesUsers.AsNoTracking()
            .Join(context.Users.AsNoTracking(),
                bsu => bsu.UserId,
                u => u.Id,
                (bsu, u) => new BrachSalesUserComboResponse(
                    bsu.Id,
                    bsu.BranchSalesId,
                    bsu.UserId,
                    u.Name,
                    bsu.Status
                ))
            .ToListAsync();

            var response = new ApiResponse<List<BrachSalesUserComboResponse>>
            {
                Data = brachSalesUsers,
                Message = "Lista de sucursales de usuario",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
