using Microsoft.EntityFrameworkCore;

public static class UpdateUserRolesEndpoint
{
    public static void MapUpdateUserRoles(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}/roles", async (
            int id,
            UpdateUserRolesRequest request,
            RecepcionDbContext db) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return Results.NotFound("Usuario no encontrado.");
            }

            var requestedIds = request.RoleIds.Distinct().ToList();
            var existingRoleIds = await db.Roles
                .Where(x => requestedIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();

            var invalidIds = requestedIds.Except(existingRoleIds).ToList();
            if (invalidIds.Count > 0)
            {
                return Results.BadRequest($"Los siguientes roles no existen: {string.Join(", ", invalidIds)}");
            }

            var currentAssignments = await db.UserRoles
                .Where(x => x.UserId == id)
                .ToListAsync();

            db.UserRoles.RemoveRange(currentAssignments);

            foreach (var roleId in existingRoleIds)
            {
                db.UserRoles.Add(new UserRole { UserId = id, RoleId = roleId });
            }

            await db.SaveChangesAsync();

            var roles = await db.UserRoles
                .AsNoTracking()
                .Where(x => x.UserId == id)
                .OrderBy(x => x.Role.Name)
                .Select(x => new RoleResponse(
                    x.Role.Id, x.Role.Name, x.Role.Description, x.Role.IsActive, x.Role.CreatedAt))
                .ToListAsync();

            var response = new UserRolesResponse(user.Id, user.UserName, roles);

            return Results.Ok(new ApiResponse<UserRolesResponse>
            {
                Success = true,
                Message = "Roles del usuario actualizados correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
