using Microsoft.EntityFrameworkCore;

public static class GetPermissionsEndpoint
{
    public static void MapGetPermissions(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var permissions = await db.Permissions
                .AsNoTracking()
                .OrderBy(x => x.Code)
                .Select(x => new PermissionResponse(x.Id, x.Code, x.Description, x.IsActive, x.CreatedAt))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<PermissionResponse>>
            {
                Success = true,
                Message = "Listado de permisos.",
                StatusCode = StatusCodes.Status200OK,
                Data = permissions
            });
        });
    }
}
