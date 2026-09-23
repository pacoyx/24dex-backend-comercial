using Microsoft.EntityFrameworkCore;

public class GetPurchasePaymentsService : IGetPurchasePaymentsService
{
    private readonly RecepcionDbContext _context;

    public GetPurchasePaymentsService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<List<RegisterPurchasePaymentResponseDto>?> GetPaymentsAsync(int purchaseInvoiceId)
    {
        var purchaseInvoiceExists = await _context.PurchaseInvoices.AnyAsync(x => x.Id == purchaseInvoiceId);
        if (!purchaseInvoiceExists)
        {
            return null;
        }

        var payments = await _context.PurchaseInvoicePayments
            .AsNoTracking()
            .Include(x => x.PaymentMethod)
            .Where(x => x.PurchaseInvoiceId == purchaseInvoiceId)
            .OrderBy(x => x.PaymentDate)
            .Select(x => new RegisterPurchasePaymentResponseDto(
                x.Id, x.PurchaseInvoiceId, x.PaymentMethodId, x.PaymentMethod.Name, x.PaymentDate,
                x.Amount, x.OperationNumber, x.Observation))
            .ToListAsync();

        return payments;
    }
}
