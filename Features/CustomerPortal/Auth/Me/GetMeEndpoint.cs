using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetMeEndpoint
{
    public static void MapGetMe(this IEndpointRouteBuilder app)
    {
        app.MapGet("/me", async (ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var customerUserId = principal.GetCustomerUserId();
            if (customerUserId is null)
            {
                return Results.Unauthorized();
            }

            var user = await db.CustomerUsers
                .AsNoTracking()
                .Include(x => x.Customer).ThenInclude(c => c.BillingProfile)
                .Include(x => x.CustomerRole)
                .FirstOrDefaultAsync(x => x.Id == customerUserId);

            if (user is null)
            {
                return Results.NotFound();
            }

            var response = new CustomerMeResponse(
                user.CustomerId, user.Id, user.CustomerRole.Name,
                PickupGuidesEndpoints.GetCustomerDisplayName(user.Customer));

            return Results.Ok(new ApiResponse<CustomerMeResponse>
            {
                Success = true,
                Message = "Perfil obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetCustomerPortalMe")
        .Produces<ApiResponse<CustomerMeResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
