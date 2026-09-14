using Microsoft.EntityFrameworkCore;

public static class GetPaymentsEndpoint
{
    public static void MapGetPayments(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}/payments", async (long id, RecepcionDbContext db) =>
        {
            var invoiceExists = await db.Invoices.AnyAsync(x => x.Id == id);
            if (!invoiceExists)
            {
                return Results.NotFound();
            }

            var payments = await db.InvoicePayments
                .AsNoTracking()
                .Include(x => x.PaymentMethod)
                .Where(x => x.InvoiceId == id)
                .OrderBy(x => x.PaymentDate)
                .Select(x => new InvoicePaymentResponse(
                    x.Id, x.InvoiceId, x.PaymentMethodId, x.PaymentMethod.Name, x.PaymentDate,
                    x.Amount, x.OperationNumber, x.Observation, x.CollectionSheetId))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<InvoicePaymentResponse>>
            {
                Success = true,
                Message = "Pagos de la factura.",
                StatusCode = StatusCodes.Status200OK,
                Data = payments
            });
        });
    }
}
