using FluentValidation;
using System.Security.Claims;

public static class UpdatePickupMobileGuideEndpoint
{
    public static void MapUpdatePickupMobileGuide(this IEndpointRouteBuilder app)
    {
        app.MapPut("/pickup-guides/{id:long}", async (
            long id,
            UpdatePickupGuideRequest request,
            IValidator<UpdatePickupGuideRequest> validator,
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
            var mobileRequest = request with { EmployeeId = employeeId.Value };

            logger.LogInformacion("PickupMobile: usuario {0} (empleado {1}) edita la guía {2}.", principal.GetUserId(), employeeId.Value, id);

            // Reutiliza el mismo Command del Backoffice; solo se agregan las reglas propias del canal móvil:
            // la guía debe pertenecer al recolector autenticado y solo puede editarse en estado Draft.
            return await UpdatePickupGuideEndpoint.ExecuteAsync(id, mobileRequest, validator, db, guide =>
            {
                if (guide.EmployeeId != employeeId.Value)
                {
                    return Results.NotFound();
                }

                if (guide.Status != PickupGuideStatus.Draft)
                {
                    return Results.BadRequest(new[] { $"Solo se pueden editar guías en estado 'Draft' desde la aplicación móvil. Estado actual: '{guide.Status}'." });
                }

                return null;
            });
        })
        .WithName("UpdatePickupMobileGuide")
        .Produces<ApiResponse<PickupGuideResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
