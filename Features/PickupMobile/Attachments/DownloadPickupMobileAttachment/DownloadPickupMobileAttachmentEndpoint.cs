using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class DownloadPickupMobileAttachmentEndpoint
{
    public static void MapDownloadPickupMobileAttachment(this IEndpointRouteBuilder app)
    {
        app.MapGet("/attachments/{attachmentId:long}/download", async (
            long attachmentId,
            ClaimsPrincipal principal,
            RecepcionDbContext db,
            IFileStorageService storage,
            CancellationToken cancellationToken) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            // Filtrado obligatorio por EmployeeId del token: un recolector solo descarga evidencias de sus propias guías.
            var attachment = await db.PickupGuideAttachments
                .AsNoTracking()
                .Include(x => x.PickupGuide)
                .FirstOrDefaultAsync(x => x.Id == attachmentId, cancellationToken);

            if (attachment is null || attachment.PickupGuide.EmployeeId != employeeId.Value)
            {
                return Results.NotFound();
            }

            return await DownloadAttachmentEndpoint.ExecuteAsync(attachmentId, db, storage, cancellationToken);
        })
        .WithName("DownloadPickupMobileAttachment")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
