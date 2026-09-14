using Microsoft.EntityFrameworkCore;

public static class CancelInvoiceEndpoint
{
    public static void MapCancelInvoice(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/cancel", async (long id, RecepcionDbContext db) =>
        {
            var invoice = await db.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            if (invoice is null)
            {
                return Results.NotFound();
            }

            if (invoice.Status == InvoiceStatus.Cancelled)
            {
                return Results.BadRequest("La factura ya se encuentra anulada.");
            }

            if (invoice.AmountPaid > 0)
            {
                return Results.BadRequest("No se puede anular una factura que ya tiene pagos registrados.");
            }

            invoice.Status = InvoiceStatus.Cancelled;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Factura anulada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { invoice.Id, invoice.Status }
            });
        });
    }
}
