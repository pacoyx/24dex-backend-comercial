using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetPickupMobileGuideEndpoint
{
    public static void MapGetPickupMobileGuide(this IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-guides/{id:long}", async (long id, ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            // Filtrado obligatorio por EmployeeId del token: un recolector solo consulta sus propias guías.
            var guide = await db.PickupGuides
                .AsNoTracking()
                .Include(x => x.Customer).ThenInclude(c => c.BillingProfile)
                .Include(x => x.Details).ThenInclude(d => d.ServiceItem)
                .Include(x => x.Employee)
                .Include(x => x.Vehicle)
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.Id == id && x.EmployeeId == employeeId.Value);

            if (guide is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new ApiResponse<PickupGuideResponse>
            {
                Success = true,
                Message = "Guía de recojo obtenida correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = PickupGuidesEndpoints.ToResponse(guide)
            });
        })
        .WithName("GetPickupMobileGuide")
        .Produces<ApiResponse<PickupGuideResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
