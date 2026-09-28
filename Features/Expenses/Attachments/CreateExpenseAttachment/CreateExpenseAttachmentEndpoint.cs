using Microsoft.EntityFrameworkCore;

public static class CreateExpenseAttachmentEndpoint
{
    public static void MapCreateExpenseAttachment(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/attachments", async (
            long id,
            CreateExpenseAttachmentRequest request,
            RecepcionDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(request.FileName) || string.IsNullOrWhiteSpace(request.BlobUrl))
            {
                return Results.BadRequest("El nombre de archivo y la URL del adjunto son obligatorios.");
            }

            var expenseExists = await db.Expenses.AnyAsync(x => x.Id == id);
            if (!expenseExists)
            {
                return Results.NotFound("Gasto no encontrado.");
            }

            var attachment = new ExpenseAttachment
            {
                ExpenseId = id,
                FileName = request.FileName,
                OriginalFileName = request.OriginalFileName,
                BlobUrl = request.BlobUrl,
                CreatedAt = DateTime.UtcNow
            };

            await db.ExpenseAttachments.AddAsync(attachment);
            await db.SaveChangesAsync();

            var response = new ApiResponse<ExpenseAttachmentResponse>
            {
                Success = true,
                Message = "Adjunto registrado correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = new ExpenseAttachmentResponse(
                    attachment.Id,
                    attachment.ExpenseId,
                    attachment.FileName,
                    attachment.OriginalFileName,
                    attachment.BlobUrl,
                    attachment.CreatedAt)
            };

            return Results.Created($"/api/expenses/{id}/attachments", response);
        });
    }
}
