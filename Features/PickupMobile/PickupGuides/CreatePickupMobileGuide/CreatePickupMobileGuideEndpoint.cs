using FluentValidation;
using System.Security.Claims;

public static class CreatePickupMobileGuideEndpoint
{
    public static void MapCreatePickupMobileGuide(this IEndpointRouteBuilder app)
    {
        app.MapPost("/pickup-guides", async (
            CreatePickupGuideRequest request,
            IValidator<CreatePickupGuideRequest> validator,
            ClaimsPrincipal principal,
            RecepcionDbContext db,
            IAppLogger<string> logger) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            // El recolector autenticado queda forzado como EmployeeId de la guía; nunca se recibe del cliente.
            // Reutiliza el mismo Command del Backoffice (que ya fuerza Status = Draft al crear).
            var mobileRequest = request with { EmployeeId = employeeId.Value };

            logger.LogInformacion("PickupMobile: usuario {0} (empleado {1}) crea una guía de recojo.", principal.GetUserId(), employeeId.Value);

            return await CreatePickupGuideEndpoint.ExecuteAsync(mobileRequest, validator, db);
        })
        .WithName("CreatePickupMobileGuide")
        .Produces<ApiResponse<PickupGuideResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
