using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class DownloadPortalPickupGuideAttachmentEndpoint
{
    public static void MapDownloadPortalPickupGuideAttachment(this IEndpointRouteBuilder app)
    {
        app.MapGet("/attachments/{attachmentId:long}/download", async (
            long attachmentId,
            ClaimsPrincipal principal,
            RecepcionDbContext db,
            IFileStorageService storage,
            CancellationToken cancellationToken) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            // Filtrado obligatorio por CustomerId del token: nunca por el request.
            var attachment = await db.PickupGuideAttachments
                .AsNoTracking()
                .Include(x => x.PickupGuide)
                .FirstOrDefaultAsync(x => x.Id == attachmentId
                    && x.IsCurrent
                    && x.PickupGuide.CustomerId == customerId.Value, cancellationToken);

            if (attachment is null)
            {
                return Results.NotFound();
            }

            var stream = await storage.OpenReadAsync(attachment.FilePath, cancellationToken);

            return Results.File(stream, attachment.ContentType, enableRangeProcessing: true);
        })
        .WithName("DownloadPortalPickupGuideAttachment")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .RequireCustomerPermission(CustomerPermissionCodes.GuidesView);
    }
}
