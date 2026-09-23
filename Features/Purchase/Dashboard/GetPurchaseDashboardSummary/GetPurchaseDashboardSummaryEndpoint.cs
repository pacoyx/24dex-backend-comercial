using Microsoft.EntityFrameworkCore;

public static class GetPurchaseDashboardSummaryEndpoint
{
    public static void MapGetPurchaseDashboardSummary(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var monthInvoices = await db.PurchaseInvoices
                .AsNoTracking()
                .Where(x => x.InvoiceIssueDate >= monthStart
                    && x.InvoiceIssueDate < monthEnd
                    && x.PaymentStatus != PurchaseInvoiceStatus.Cancelled)
                .Select(x => new { x.Total, x.SupplierId })
                .ToListAsync();

            var accountsPayable = await db.PurchaseInvoices
                .AsNoTracking()
                .Where(x => x.PaymentStatus == PurchaseInvoiceStatus.Pending || x.PaymentStatus == PurchaseInvoiceStatus.PartiallyPaid)
                .SumAsync(x => (decimal?)x.Balance) ?? 0;

            var response = new PurchaseDashboardSummaryResponseDto(
                monthInvoices.Sum(x => x.Total),
                accountsPayable,
                monthInvoices.Count,
                monthInvoices.Select(x => x.SupplierId).Distinct().Count()
            );

            return Results.Ok(new ApiResponse<PurchaseDashboardSummaryResponseDto>
            {
                Success = true,
                Message = "Resumen del dashboard de compras.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
