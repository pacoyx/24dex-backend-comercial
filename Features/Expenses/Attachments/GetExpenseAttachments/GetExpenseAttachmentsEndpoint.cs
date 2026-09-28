using Microsoft.EntityFrameworkCore;

public static class GetExpenseAttachmentsEndpoint
{
    public static void MapGetExpenseAttachments(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}/attachments", async (long id, RecepcionDbContext db) =>
        {
            var expenseExists = await db.Expenses.AnyAsync(x => x.Id == id);
            if (!expenseExists)
            {
                return Results.NotFound("Gasto no encontrado.");
            }

            var attachments = await db.ExpenseAttachments
                .AsNoTracking()
                .Where(x => x.ExpenseId == id)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new ExpenseAttachmentResponse(
                    x.Id, x.ExpenseId, x.FileName, x.OriginalFileName, x.BlobUrl, x.CreatedAt))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<ExpenseAttachmentResponse>>
            {
                Success = true,
                Message = "Adjuntos del gasto.",
                StatusCode = StatusCodes.Status200OK,
                Data = attachments
            });
        });
    }
}
