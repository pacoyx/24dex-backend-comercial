using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetCurrentUserEndpoint
{
    public static void MapGetCurrentUser(this IEndpointRouteBuilder app)
    {
        app.MapGet("/me", async (ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var userId = principal.GetUserId();
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return Results.NotFound();
            }

            var roles = principal.FindAll("roles").Select(x => x.Value).ToList();
            var permissions = principal.FindAll("permissions").Select(x => x.Value).ToList();

            var response = new CurrentUserResponse(user.Id, user.Name, roles, permissions);

            return Results.Ok(new ApiResponse<CurrentUserResponse>
            {
                Success = true,
                Message = "Usuario actual.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .RequireAuthorization();
    }
}
