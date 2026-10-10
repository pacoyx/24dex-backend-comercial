using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class GenerateActivationLinkEndpoint
{
    public static void MapGenerateActivationLink(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users/{id:long}/generate-activation-link", async (
            long id,
            ClaimsPrincipal principal,
            RecepcionDbContext db,
            IOptions<CustomerPortalSettings> portalSettings) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var user = await db.CustomerUsers
                .FirstOrDefaultAsync(x => x.Id == id && x.CustomerId == customerId.Value);

            if (user is null)
            {
                return Results.NotFound();
            }

            var pendingTokens = await db.CustomerUserTokens
                .Where(x => x.CustomerUserId == id && x.Purpose == CustomerUserTokenPurpose.Activation && !x.IsUsed)
                .ToListAsync();
            foreach (var pending in pendingTokens)
            {
                pending.IsUsed = true;
            }

            var token = new CustomerUserToken
            {
                CustomerUserId = id,
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
                Message = "Enlace de activación generado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new GenerateActivationLinkResponse(activationUrl)
            });
        })
        .WithName("GenerateCustomerActivationLink")
        .Produces<ApiResponse<GenerateActivationLinkResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireCustomerPermission(CustomerPermissionCodes.UsersManage);
    }
}
