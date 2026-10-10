using Microsoft.EntityFrameworkCore;

public static class GetAttachmentsEndpoint
{
    public static void MapGetAttachments(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}/attachments", async (long id, PickupGuideAttachmentType? attachmentType, RecepcionDbContext db) =>
        {
            var guideExists = await db.PickupGuides.AnyAsync(x => x.Id == id);
            if (!guideExists)
            {
                return Results.NotFound("Guía no encontrada.");
            }

            var query = db.PickupGuideAttachments.AsNoTracking().Where(x => x.PickupGuideId == id);
            if (attachmentType.HasValue)
            {
                query = query.Where(x => x.AttachmentType == attachmentType.Value);
            }

            var attachments = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();

            return Results.Ok(new ApiResponse<List<PickupGuideAttachmentResponse>>
            {
                Success = true,
                Message = "Listado de evidencias.",
                StatusCode = StatusCodes.Status200OK,
                Data = attachments.Select(PickupGuideAttachmentsEndpoints.ToResponse).ToList()
            });
        })
        .WithName("GetPickupGuideAttachments")
        .Produces<ApiResponse<List<PickupGuideAttachmentResponse>>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
