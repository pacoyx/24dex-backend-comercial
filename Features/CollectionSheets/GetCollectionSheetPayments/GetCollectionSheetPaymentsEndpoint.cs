using Microsoft.EntityFrameworkCore;

public static class GetCollectionSheetPaymentsEndpoint
{
    public static void MapGetCollectionSheetPayments(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}/payments", async (long id, RecepcionDbContext db) =>
        {
            var sheetExists = await db.CollectionSheets.AnyAsync(x => x.Id == id);
            if (!sheetExists)
            {
                return Results.NotFound();
            }

            var payments = await db.InvoicePayments
                .AsNoTracking()
                .Include(x => x.PaymentMethod)
                .Where(x => x.CollectionSheetId == id)
                .OrderBy(x => x.PaymentDate)
                .Select(x => new InvoicePaymentResponse(
                    x.Id, x.InvoiceId, x.PaymentMethodId, x.PaymentMethod.Name, x.PaymentDate,
                    x.Amount, x.OperationNumber, x.Observation, x.CollectionSheetId))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<InvoicePaymentResponse>>
            {
                Success = true,
                Message = "Pagos de la planilla.",
                StatusCode = StatusCodes.Status200OK,
                Data = payments
            });
        });
    }
}
