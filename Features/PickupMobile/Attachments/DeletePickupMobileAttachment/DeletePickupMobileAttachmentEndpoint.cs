using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class DeletePickupMobileAttachmentEndpoint
{
    public static void MapDeletePickupMobileAttachment(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/attachments/{attachmentId:long}", async (
            long attachmentId,
            ClaimsPrincipal principal,
            RecepcionDbContext db,
            IFileStorageService storage,
            IAppLogger<string> logger) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            // Filtrado obligatorio por EmployeeId del token: un recolector solo elimina evidencias de sus propias guías.
            var attachment = await db.PickupGuideAttachments
                .AsNoTracking()
                .Include(x => x.PickupGuide)
                .FirstOrDefaultAsync(x => x.Id == attachmentId);

            if (attachment is null || attachment.PickupGuide.EmployeeId != employeeId.Value)
            {
                return Results.NotFound();
            }

            logger.LogInformacion("PickupMobile: usuario {0} (empleado {1}) elimina evidencia {2} de la guía {3}.",
                principal.GetUserId(), employeeId.Value, attachmentId, attachment.PickupGuideId);

            return await DeleteAttachmentEndpoint.ExecuteAsync(attachmentId, db, storage);
        })
        .WithName("DeletePickupMobileAttachment")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
