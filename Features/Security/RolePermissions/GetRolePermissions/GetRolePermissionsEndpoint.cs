using Microsoft.EntityFrameworkCore;

public static class GetRolePermissionsEndpoint
{
    public static void MapGetRolePermissions(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/permissions", async (int id, RecepcionDbContext db) =>
        {
            var role = await db.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (role is null)
            {
                return Results.NotFound("Rol no encontrado.");
            }

            var permissions = await db.RolePermissions
                .AsNoTracking()
                .Where(x => x.RoleId == id)
                .OrderBy(x => x.Permission.Code)
                .Select(x => new PermissionResponse(
                    x.Permission.Id, x.Permission.Code, x.Permission.Description,
                    x.Permission.IsActive, x.Permission.CreatedAt))
                .ToListAsync();

            var response = new RolePermissionsResponse(role.Id, role.Name, permissions);

            return Results.Ok(new ApiResponse<RolePermissionsResponse>
            {
                Success = true,
                Message = "Permisos del rol.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
