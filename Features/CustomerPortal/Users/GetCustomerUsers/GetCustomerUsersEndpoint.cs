using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetCustomerUsersEndpoint
{
    public static void MapGetCustomerUsers(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var users = await db.CustomerUsers
                .AsNoTracking()
                .Include(x => x.CustomerRole)
                .Where(x => x.CustomerId == customerId.Value)
                .OrderBy(x => x.FullName)
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<CustomerUserResponse>>
            {
                Success = true,
                Message = "Listado de usuarios de la empresa.",
                StatusCode = StatusCodes.Status200OK,
                Data = users.Select(CustomerUsersEndpoints.ToResponse).ToList()
            });
        })
        .WithName("GetCustomerPortalUsers")
        .Produces<ApiResponse<List<CustomerUserResponse>>>(StatusCodes.Status200OK)
        .RequireCustomerPermission(CustomerPermissionCodes.UsersManage);
    }
}
