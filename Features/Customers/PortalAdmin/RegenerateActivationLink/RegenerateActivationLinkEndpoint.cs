using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class RegenerateActivationLinkEndpoint
{
    public static void MapRegenerateActivationLink(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{customerId:int}/portal-admin/regenerate-activation-link", async (
            int customerId,
            RecepcionDbContext db,
            IOptions<CustomerPortalSettings> portalSettings) =>
        {
            var customerExists = await db.Customers.AnyAsync(x => x.Id == customerId);
            if (!customerExists)
            {
                return Results.NotFound("Cliente no encontrado.");
            }

            // El administrador bootstrap es el usuario mas antiguo del cliente que aun no activa su cuenta.
            var pendingAdmin = await db.CustomerUsers
                .Where(x => x.CustomerId == customerId && !x.IsActive)
                .OrderBy(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (pendingAdmin is null)
            {
                return Results.BadRequest(new[] { "No hay un administrador pendiente de activación para este cliente." });
            }

            var pendingTokens = await db.CustomerUserTokens
                .Where(x => x.CustomerUserId == pendingAdmin.Id && x.Purpose == CustomerUserTokenPurpose.Activation && !x.IsUsed)
                .ToListAsync();
            foreach (var pending in pendingTokens)
            {
                pending.IsUsed = true;
            }

            var token = new CustomerUserToken
            {
                CustomerUserId = pendingAdmin.Id,
                Token = CustomerPortalTokenGenerator.Generate(),
                Purpose = CustomerUserTokenPurpose.Activation,
                ExpiresAt = DateTime.UtcNow.AddHours(48),
                IsUsed = false
            };

            await db.CustomerUserTokens.AddAsync(token);
            await db.SaveChangesAsync();

            var activationUrl = $"{portalSettings.Value.BaseUrl.TrimEnd('/')}/activate-account?token={Uri.EscapeDataString(token.Token)}";

            return Results.Ok(new ApiResponse<GenerateActivationLinkResponse>
            {
                Success = true,
                Message = "Enlace de activación regenerado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new GenerateActivationLinkResponse(activationUrl)
            });
        })
        .WithName("RegenerateCustomerPortalActivationLink")
        .Produces<ApiResponse<GenerateActivationLinkResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
