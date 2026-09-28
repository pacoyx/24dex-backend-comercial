using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateRoleEndpoint
{
    public static void MapCreateRole(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateRoleRequest request,
            IValidator<CreateRoleRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var nameInUse = await db.Roles.AnyAsync(x => x.Name == request.Name);
            if (nameInUse)
            {
                return Results.BadRequest("Ya existe un rol con ese nombre.");
            }

            var role = new Role
            {
                Name = request.Name,
                Description = request.Description ?? string.Empty,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await db.Roles.AddAsync(role);
            await db.SaveChangesAsync();

            var response = new ApiResponse<RoleResponse>
            {
                Success = true,
                Message = "Rol creado correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = new RoleResponse(role.Id, role.Name, role.Description, role.IsActive, role.CreatedAt)
            };

            return Results.Created($"/api/roles/{role.Id}", response);
        });
    }
}
