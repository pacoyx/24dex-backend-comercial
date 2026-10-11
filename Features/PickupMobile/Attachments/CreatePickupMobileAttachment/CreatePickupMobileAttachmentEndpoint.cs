using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class CreatePickupMobileAttachmentEndpoint
{
    private static readonly HashSet<string> AllowedSignatureContentTypes = new(StringComparer.OrdinalIgnoreCase) { "image/png" };
    private static readonly HashSet<string> AllowedPhotoContentTypes = new(StringComparer.OrdinalIgnoreCase) { "image/jpeg", "image/png", "image/webp" };

    public static void MapCreatePickupMobileAttachment(this IEndpointRouteBuilder app)
    {
        app.MapPost("/pickup-guides/{id:long}/attachments", async (
            long id,
            [FromForm] PickupGuideAttachmentType attachmentType,
            IFormFile file,
            ClaimsPrincipal principal,
            RecepcionDbContext db,
            IFileStorageService storage,
            IOptions<PickupMobileSettings> settings,
            IAppLogger<string> logger) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            if (attachmentType == PickupGuideAttachmentType.Pdf)
            {
                return Results.BadRequest(new[] { "La aplicación móvil no puede subir constancias PDF; estas se generan automáticamente en el Backoffice." });
            }

            var allowedContentTypes = attachmentType == PickupGuideAttachmentType.Signature
                ? AllowedSignatureContentTypes
                : AllowedPhotoContentTypes;

            if (!allowedContentTypes.Contains(file.ContentType))
            {
                return Results.BadRequest(new[] { $"Tipo de archivo no permitido para '{attachmentType}'. Formatos aceptados: {string.Join(", ", allowedContentTypes)}." });
            }

            var maxSizeBytes = settings.Value.MaxAttachmentSizeMb * 1024L * 1024L;
            if (file.Length > maxSizeBytes)
            {
                return Results.BadRequest(new[] { $"El archivo supera el tamaño máximo permitido de {settings.Value.MaxAttachmentSizeMb} MB." });
            }

            // Filtrado obligatorio por EmployeeId del token: un recolector solo adjunta evidencias a sus propias guías.
            var guideExists = await db.PickupGuides.AnyAsync(x => x.Id == id && x.EmployeeId == employeeId.Value);
            if (!guideExists)
            {
                return Results.NotFound();
            }

            logger.LogInformacion("PickupMobile: usuario {0} (empleado {1}) sube evidencia tipo {2} a la guía {3}.",
                principal.GetUserId(), employeeId.Value, attachmentType, id);

            // Reutiliza el mismo almacenamiento/creación del Backoffice (Local o Azure Blob, según configuración).
            return await CreateAttachmentEndpoint.ExecuteAsync(id, attachmentType, file, db, storage);
        })
        .DisableAntiforgery()
        .WithName("CreatePickupMobileAttachment")
        .Produces<ApiResponse<PickupGuideAttachmentResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
