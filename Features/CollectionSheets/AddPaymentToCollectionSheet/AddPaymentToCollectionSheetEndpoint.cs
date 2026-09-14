using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class AddPaymentToCollectionSheetEndpoint
{
    public static void MapAddPaymentToCollectionSheet(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/payments", async (
            long id,
            AddPaymentToCollectionSheetRequest request,
            IValidator<AddPaymentToCollectionSheetRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var sheet = await db.CollectionSheets.FirstOrDefaultAsync(x => x.Id == id);
            if (sheet is null)
            {
                return Results.NotFound("Planilla no encontrada.");
            }

            if (sheet.Status == CollectionSheetStatus.Closed)
            {
                return Results.BadRequest("La planilla está cerrada, no se pueden agregar más pagos.");
            }

            var invoice = await db.Invoices.FirstOrDefaultAsync(x => x.Id == request.InvoiceId);
            if (invoice is null)
            {
                return Results.NotFound("Factura no encontrada.");
            }

            var paymentMethod = await db.PaymentMethods
                .FirstOrDefaultAsync(x => x.Id == request.PaymentMethodId && x.IsActive);
            if (paymentMethod is null)
            {
                return Results.BadRequest("Método de pago inválido o inactivo.");
            }

            var error = InvoicePaymentRules.Validate(invoice, request.Amount);
            if (error is not null)
            {
                return Results.BadRequest(error);
            }

            var payment = new InvoicePayment
            {
                InvoiceId = invoice.Id,
                PaymentMethodId = request.PaymentMethodId,
                PaymentDate = DateTime.UtcNow,
                Amount = request.Amount,
                OperationNumber = request.OperationNumber,
                Observation = request.Observation,
                CollectionSheetId = sheet.Id
            };

            InvoicePaymentRules.Apply(invoice, request.Amount);
            sheet.TotalAmount += request.Amount;

            await db.InvoicePayments.AddAsync(payment);
            await db.SaveChangesAsync();

            var response = new InvoicePaymentResponse(
                payment.Id, payment.InvoiceId, payment.PaymentMethodId, paymentMethod.Name,
                payment.PaymentDate, payment.Amount, payment.OperationNumber, payment.Observation,
                payment.CollectionSheetId);

            return Results.Ok(new ApiResponse<InvoicePaymentResponse>
            {
                Success = true,
                Message = "Pago agregado a la planilla correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .RequireAuthorization();
    }
}
