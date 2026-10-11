using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetMyPickupGuidesEndpoint
{
    public static void MapGetMyPickupGuides(this IEndpointRouteBuilder app)
    {
        app.MapGet("/my-pickup-guides", async (ClaimsPrincipal principal, PickupGuideStatus? status, RecepcionDbContext db) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            // Filtrado obligatorio por EmployeeId del token: nunca se recibe por parámetro.
            var query = db.PickupGuides
                .AsNoTracking()
                .Include(x => x.Customer).ThenInclude(c => c.BillingProfile)
                .Include(x => x.Attachments)
                .Where(x => x.EmployeeId == employeeId.Value)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            var guides = await query.OrderByDescending(x => x.PickupDate).ToListAsync();

            return Results.Ok(new ApiResponse<List<PickupMobileGuideDto>>
            {
                Success = true,
                Message = "Mis guías de recojo.",
                StatusCode = StatusCodes.Status200OK,
                Data = guides.Select(PickupMobileEndpoints.ToGuideDto).ToList()
            });
        })
        .WithName("GetMyPickupGuides")
        .Produces<ApiResponse<List<PickupMobileGuideDto>>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
