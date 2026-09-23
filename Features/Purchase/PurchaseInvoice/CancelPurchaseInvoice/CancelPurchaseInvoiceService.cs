using Microsoft.EntityFrameworkCore;

public class CancelPurchaseInvoiceService : ICancelPurchaseInvoiceService
{
    private readonly RecepcionDbContext _context;

    public CancelPurchaseInvoiceService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<CancelPurchaseInvoiceResult> CancelPurchaseInvoiceAsync(int purchaseInvoiceId)
    {
        var purchaseInvoice = await _context.PurchaseInvoices.FirstOrDefaultAsync(x => x.Id == purchaseInvoiceId);
        if (purchaseInvoice == null)
        {
            return new CancelPurchaseInvoiceResult { NotFound = true };
        }

        if (purchaseInvoice.PaymentStatus == PurchaseInvoiceStatus.Cancelled)
        {
            return new CancelPurchaseInvoiceResult { Error = "La compra ya se encuentra anulada." };
        }

        if (purchaseInvoice.AmountPaid > 0)
        {
            return new CancelPurchaseInvoiceResult { Error = "No se puede anular una compra que ya tiene pagos registrados." };
        }

        purchaseInvoice.PaymentStatus = PurchaseInvoiceStatus.Cancelled;
        await _context.SaveChangesAsync();

        return new CancelPurchaseInvoiceResult { PaymentStatus = purchaseInvoice.PaymentStatus };
    }
}
