using Microsoft.EntityFrameworkCore;

public static class UpdateRolePermissionsEndpoint
{
    public static void MapUpdateRolePermissions(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}/permissions", async (
            int id,
            UpdateRolePermissionsRequest request,
            RecepcionDbContext db) =>
        {
            var role = await db.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (role is null)
            {
                return Results.NotFound("Rol no encontrado.");
            }

            var requestedIds = request.PermissionIds.Distinct().ToList();
            var existingPermissionIds = await db.Permissions
                .Where(x => requestedIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();

            var invalidIds = requestedIds.Except(existingPermissionIds).ToList();
            if (invalidIds.Count > 0)
            {
                return Results.BadRequest($"Los siguientes permisos no existen: {string.Join(", ", invalidIds)}");
            }

            var currentAssignments = await db.RolePermissions
                .Where(x => x.RoleId == id)
                .ToListAsync();

            db.RolePermissions.RemoveRange(currentAssignments);

            foreach (var permissionId in existingPermissionIds)
            {
                db.RolePermissions.Add(new RolePermission { RoleId = id, PermissionId = permissionId });
            }

            await db.SaveChangesAsync();

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
                Message = "Permisos del rol actualizados correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
