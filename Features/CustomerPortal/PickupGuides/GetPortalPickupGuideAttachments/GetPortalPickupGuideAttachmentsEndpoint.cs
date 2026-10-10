using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetPortalPickupGuideAttachmentsEndpoint
{
    public static void MapGetPortalPickupGuideAttachments(this IEndpointRouteBuilder app)
    {
        app.MapGet("/pickup-guides/{id:long}/attachments", async (long id, ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            // Filtrado obligatorio por CustomerId del token: nunca por el request.
            var guideExists = await db.PickupGuides
                .AnyAsync(x => x.Id == id && x.CustomerId == customerId.Value);

            if (!guideExists)
            {
                return Results.NotFound();
            }

            var attachments = await db.PickupGuideAttachments
                .AsNoTracking()
                .Where(x => x.PickupGuideId == id && x.IsCurrent)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new PortalPickupGuideAttachmentResponse(x.Id, x.AttachmentType, x.OriginalFileName, x.ContentType, x.FileSize, x.CreatedAt))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<PortalPickupGuideAttachmentResponse>>
            {
                Success = true,
                Message = "Evidencias de la guía de recojo.",
                StatusCode = StatusCodes.Status200OK,
                Data = attachments
            });
        })
        .WithName("GetPortalPickupGuideAttachments")
        .Produces<ApiResponse<List<PortalPickupGuideAttachmentResponse>>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .RequireCustomerPermission(CustomerPermissionCodes.GuidesView);
    }
}
