using Microsoft.EntityFrameworkCore;

public static class DeleteRoleEndpoint
{
    public static void MapDeleteRole(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", async (int id, RecepcionDbContext db) =>
        {
            var role = await db.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (role is null)
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Rol no encontrado.",
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = "Rol no encontrado."
                });
            }

            role.IsActive = false;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Rol desactivado.",
                StatusCode = StatusCodes.Status200OK,
                Data = "Rol desactivado."
            });
        });
    }
}
