using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetRolesEndpoint
{
    public static void MapGetRoles(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            [FromQuery] bool? isActive,
            RecepcionDbContext db) =>
        {
            var query = db.Roles.AsNoTracking().AsQueryable();

            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive.Value);
            }

            var roles = await query
                .OrderBy(x => x.Name)
                .Select(x => new RoleResponse(x.Id, x.Name, x.Description, x.IsActive, x.CreatedAt))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<RoleResponse>>
            {
                Success = true,
                Message = "Listado de roles.",
                StatusCode = StatusCodes.Status200OK,
                Data = roles
            });
        });
    }
}
