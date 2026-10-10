using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class EnablePortalEndpoint
{
    public static void MapEnablePortal(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{customerId:int}/portal-enable", async (
            int customerId,
            EnablePortalRequest request,
            IValidator<EnablePortalRequest> validator,
            RecepcionDbContext db,
            IOptions<CustomerPortalSettings> portalSettings) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var customer = await db.Customers
                .Include(x => x.BillingProfile)
                .FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer is null)
            {
                return Results.NotFound("Cliente no encontrado.");
            }

            var administratorExists = await db.CustomerUsers
                .Include(x => x.CustomerRole)
                .AnyAsync(x => x.CustomerId == customerId && x.CustomerRole.Name == CustomerRoleNames.AdministradorEmpresa);

            if (customer.PortalEnabled || administratorExists)
            {
                return Results.BadRequest(new[] { "El portal ya se encuentra habilitado para este cliente." });
            }

            var emailTaken = await db.CustomerUsers.AnyAsync(x => x.Email == request.Email);
            if (emailTaken)
            {
                return Results.BadRequest(new[] { "El correo ya está registrado por otro usuario del portal." });
            }

            var adminRole = await db.CustomerRoles
                .FirstOrDefaultAsync(x => x.Name == CustomerRoleNames.AdministradorEmpresa && x.IsActive);
            if (adminRole is null)
            {
                return Results.Problem("No se encontró el rol 'Administrador Empresa' configurado en el sistema.", statusCode: 500);
            }

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            customer.PortalEnabled = true;

            // El nombre de usuario se inicializa igual al correo; el administrador podra
            // crear al resto de usuarios de su empresa una vez active su cuenta.
            var adminUser = new CustomerUser
            {
                CustomerId = customerId,
                CustomerRoleId = adminRole.Id,
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email,
                PasswordHash = string.Empty,
                IsActive = false,
                CreatedAt = fechaOperacion
            };

            await db.CustomerUsers.AddAsync(adminUser);
            await db.SaveChangesAsync();

            var token = new CustomerUserToken
            {
                CustomerUserId = adminUser.Id,
                Token = CustomerPortalTokenGenerator.Generate(),
                Purpose = CustomerUserTokenPurpose.Activation,
                ExpiresAt = DateTime.UtcNow.AddHours(48),
                IsUsed = false
            };

            await db.CustomerUserTokens.AddAsync(token);
            await db.SaveChangesAsync();

            var activationUrl = $"{portalSettings.Value.BaseUrl.TrimEnd('/')}/activate-account?token={Uri.EscapeDataString(token.Token)}";

            var response = new ApiResponse<EnablePortalResponse>
            {
                Success = true,
                Message = "Portal habilitado correctamente. Comparta el enlace de activación con el administrador de la empresa.",
                StatusCode = StatusCodes.Status201Created,
                Data = new EnablePortalResponse(
                    customer.Id,
                    PickupGuidesEndpoints.GetCustomerDisplayName(customer),
                    customer.PortalEnabled,
                    activationUrl)
            };

            return Results.Created($"/api/customers/{customerId}/portal-status", response);
        })
        .WithName("EnableCustomerPortal")
        .Produces<ApiResponse<EnablePortalResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
