using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class ResetPasswordEndpoint
{
    public static void MapResetPassword(this IEndpointRouteBuilder app)
    {
        app.MapPost("/reset-password", async (
            ResetPasswordRequest request,
            IValidator<ResetPasswordRequest> validator,
            RecepcionDbContext db,
            IEncryptService encryptService) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var token = await db.CustomerUserTokens
                .FirstOrDefaultAsync(x => x.Token == request.Token && x.Purpose == CustomerUserTokenPurpose.PasswordReset);

            if (token is null || token.IsUsed || token.ExpiresAt < DateTime.UtcNow)
            {
                return Results.BadRequest(new[] { "El enlace de recuperación es inválido o ha expirado." });
            }

            var user = await db.CustomerUsers.FirstOrDefaultAsync(x => x.Id == token.CustomerUserId);
            if (user is null)
            {
                return Results.NotFound();
            }

            user.PasswordHash = encryptService.HashPassword(request.NewPassword);
            user.RefreshToken = null;
            token.IsUsed = true;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Contraseña actualizada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { }
            });
        })
        .WithName("ResetCustomerPassword")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .AllowAnonymous();
    }
}
