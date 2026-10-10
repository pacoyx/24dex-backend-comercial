using Microsoft.EntityFrameworkCore;

public static class DeleteAttachmentEndpoint
{
    public static void MapDeleteAttachment(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/attachments/{attachmentId:long}", async (
            long attachmentId,
            RecepcionDbContext db,
            IFileStorageService storage) =>
        {
            var attachment = await db.PickupGuideAttachments.FirstOrDefaultAsync(x => x.Id == attachmentId);
            if (attachment is null)
            {
                return Results.NotFound("Evidencia no encontrada.");
            }

            if (attachment.AttachmentType == PickupGuideAttachmentType.Pdf && attachment.IsCurrent)
            {
                return Results.BadRequest(new[] { "No se puede eliminar la constancia PDF vigente." });
            }

            await storage.DeleteAsync(attachment.FilePath);

            db.PickupGuideAttachments.Remove(attachment);
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Evidencia eliminada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { attachment.Id }
            });
        })
        .WithName("DeletePickupGuideAttachment")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
