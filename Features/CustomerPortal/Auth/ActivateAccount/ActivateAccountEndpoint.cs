using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class ActivateAccountEndpoint
{
    public static void MapActivateAccount(this IEndpointRouteBuilder app)
    {
        app.MapPost("/activate-account", async (
            ActivateAccountRequest request,
            IValidator<ActivateAccountRequest> validator,
            RecepcionDbContext db,
            IEncryptService encryptService) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var token = await db.CustomerUserTokens
                .FirstOrDefaultAsync(x => x.Token == request.Token && x.Purpose == CustomerUserTokenPurpose.Activation);

            if (token is null || token.IsUsed || token.ExpiresAt < DateTime.UtcNow)
            {
                return Results.BadRequest(new[] { "El enlace de activación es inválido o ha expirado." });
            }

            var user = await db.CustomerUsers.FirstOrDefaultAsync(x => x.Id == token.CustomerUserId);
            if (user is null)
            {
                return Results.NotFound();
            }

            user.PasswordHash = encryptService.HashPassword(request.Password);
            user.IsActive = true;
            token.IsUsed = true;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Cuenta activada correctamente. Ya puede iniciar sesión.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { }
            });
        })
        .WithName("ActivateCustomerAccount")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .AllowAnonymous();
    }
}
