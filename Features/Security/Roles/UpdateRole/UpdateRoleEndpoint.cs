using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class UpdateRoleEndpoint
{
    public static void MapUpdateRole(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", async (
            int id,
            UpdateRoleRequest request,
            IValidator<UpdateRoleRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var role = await db.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (role is null)
            {
                return Results.NotFound("Rol no encontrado.");
            }

            var nameInUse = await db.Roles.AnyAsync(x => x.Name == request.Name && x.Id != id);
            if (nameInUse)
            {
                return Results.BadRequest("Ya existe un rol con ese nombre.");
            }

            role.Name = request.Name;
            role.Description = request.Description ?? string.Empty;
            role.IsActive = request.IsActive;

            await db.SaveChangesAsync();

            var response = new ApiResponse<RoleResponse>
            {
                Success = true,
                Message = "Rol actualizado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new RoleResponse(role.Id, role.Name, role.Description, role.IsActive, role.CreatedAt)
            };

            return Results.Ok(response);
        });
    }
}
