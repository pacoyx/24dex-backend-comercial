using Microsoft.EntityFrameworkCore;

public static class AccountsPayableQueries
{
    public static async Task<List<AccountPayableResponse>> GetPendingAsync(RecepcionDbContext db)
    {
        var today = DateTime.UtcNow.Date;

        var accountsPayable = await db.PurchaseInvoices
            .AsNoTracking()
            .Include(x => x.Supplier)
            .Where(x => x.PaymentStatus == PurchaseInvoiceStatus.Pending || x.PaymentStatus == PurchaseInvoiceStatus.PartiallyPaid)
            .OrderBy(x => x.InvoiceExpirationDate)
            .Select(x => new AccountPayableResponse(
                x.SupplierId,
                x.Supplier != null ? x.Supplier.Name : string.Empty,
                x.Id,
                x.InvoiceSerie + "-" + x.InvoiceNumber,
                x.Total,
                x.AmountPaid,
                x.Balance,
                x.InvoiceExpirationDate,
                x.PaymentStatus,
                EF.Functions.DateDiffDay(x.InvoiceExpirationDate, today)
            ))
            .ToListAsync();

        return accountsPayable
            .Select(x => x with { OverdueDays = Math.Max(0, x.OverdueDays) })
            .ToList();
    }
}
