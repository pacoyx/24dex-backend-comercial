using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class ForgotPasswordEndpoint
{
    public static void MapForgotPassword(this IEndpointRouteBuilder app)
    {
        app.MapPost("/forgot-password", async (
            ForgotPasswordRequest request,
            RecepcionDbContext db,
            IOptions<CustomerPortalSettings> portalSettings) =>
        {
            var user = await db.CustomerUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (user is null || !user.IsActive)
            {
                return Results.NotFound("Usuario no encontrado.");
            }

            var pendingTokens = await db.CustomerUserTokens
                .Where(x => x.CustomerUserId == user.Id && x.Purpose == CustomerUserTokenPurpose.PasswordReset && !x.IsUsed)
                .ToListAsync();
            foreach (var pending in pendingTokens)
            {
                pending.IsUsed = true;
            }

            var token = new CustomerUserToken
            {
                CustomerUserId = user.Id,
                Token = CustomerPortalTokenGenerator.Generate(),
                Purpose = CustomerUserTokenPurpose.PasswordReset,
                ExpiresAt = DateTime.UtcNow.AddHours(2),
                IsUsed = false
            };

            await db.CustomerUserTokens.AddAsync(token);
            await db.SaveChangesAsync();

            var resetUrl = $"{portalSettings.Value.BaseUrl.TrimEnd('/')}/reset-password?token={Uri.EscapeDataString(token.Token)}";

            return Results.Ok(new ApiResponse<ForgotPasswordResponse>
            {
                Success = true,
                Message = "Enlace de recuperación generado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new ForgotPasswordResponse(resetUrl)
            });
        })
        .WithName("ForgotCustomerPassword")
        .Produces<ApiResponse<ForgotPasswordResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .AllowAnonymous();
    }
}
