using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetPickupMobileAttachmentsEndpoint
{
    public static void MapGetPickupMobileAttachments(this IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-guides/{id:long}/attachments", async (long id, ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            // Filtrado obligatorio por EmployeeId del token: un recolector solo ve evidencias de sus propias guías.
            var guideExists = await db.PickupGuides.AnyAsync(x => x.Id == id && x.EmployeeId == employeeId.Value);
            if (!guideExists)
            {
                return Results.NotFound();
            }

            var attachments = await db.PickupGuideAttachments
                .AsNoTracking()
                .Where(x => x.PickupGuideId == id && x.IsCurrent)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<PickupMobileAttachmentDto>>
            {
                Success = true,
                Message = "Evidencias de la guía.",
                StatusCode = StatusCodes.Status200OK,
                Data = attachments.Select(PickupMobileEndpoints.ToAttachmentDto).ToList()
            });
        })
        .WithName("GetPickupMobileAttachments")
        .Produces<ApiResponse<List<PickupMobileAttachmentDto>>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
