using Microsoft.EntityFrameworkCore;

public static class GetUserRolesEndpoint
{
    public static void MapGetUserRoles(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/roles", async (int id, RecepcionDbContext db) =>
        {
            var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return Results.NotFound("Usuario no encontrado.");
            }

            var roles = await db.UserRoles
                .AsNoTracking()
                .Where(x => x.UserId == id)
                .Select(x => new RoleResponse(
                    x.Role.Id, x.Role.Name, x.Role.Description, x.Role.IsActive, x.Role.CreatedAt))
                .OrderBy(x => x.Name)
                .ToListAsync();

            var response = new UserRolesResponse(user.Id, user.UserName, roles);

            return Results.Ok(new ApiResponse<UserRolesResponse>
            {
                Success = true,
                Message = "Roles del usuario.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
