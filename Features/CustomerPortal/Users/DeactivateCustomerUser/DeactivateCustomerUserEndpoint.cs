using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class DeactivateCustomerUserEndpoint
{
    public static void MapDeactivateCustomerUser(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users/{id:long}/deactivate", async (long id, ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var user = await db.CustomerUsers
                .Include(x => x.CustomerRole)
                .FirstOrDefaultAsync(x => x.Id == id && x.CustomerId == customerId.Value);

            if (user is null)
            {
                return Results.NotFound();
            }

            if (user.IsActive && user.CustomerRole.Name == CustomerRoleNames.AdministradorEmpresa)
            {
                var otherActiveAdmins = await db.CustomerUsers
                    .Include(x => x.CustomerRole)
                    .AnyAsync(x => x.CustomerId == customerId.Value
                        && x.Id != id
                        && x.IsActive
                        && x.CustomerRole.Name == CustomerRoleNames.AdministradorEmpresa);

                if (!otherActiveAdmins)
                {
                    return Results.BadRequest(new[] { "Debe existir al menos un Administrador Empresa activo." });
                }
            }

            user.IsActive = false;
            user.RefreshToken = null;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Usuario desactivado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { user.Id, user.IsActive }
            });
        })
        .WithName("DeactivateCustomerPortalUser")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireCustomerPermission(CustomerPermissionCodes.UsersManage);
    }
}
