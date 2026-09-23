using System.Globalization;
using Microsoft.EntityFrameworkCore;

public static class GetMonthlyPurchasesEndpoint
{
    public static void MapGetMonthlyPurchases(this IEndpointRouteBuilder app)
    {
        app.MapGet("/monthly-purchases", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var start = new DateTime(today.Year, today.Month, 1).AddMonths(-11);

            var invoices = await db.PurchaseInvoices
                .AsNoTracking()
                .Where(x => x.InvoiceIssueDate >= start && x.PaymentStatus != PurchaseInvoiceStatus.Cancelled)
                .Select(x => new { x.InvoiceIssueDate, x.Total })
                .ToListAsync();

            var cultura = new CultureInfo("es-ES");
            var data = new List<MonthlyPurchaseResponseDto>();
            for (var month = start; month <= today; month = month.AddMonths(1))
            {
                var amount = invoices
                    .Where(x => x.InvoiceIssueDate.Year == month.Year && x.InvoiceIssueDate.Month == month.Month)
                    .Sum(x => x.Total);

                var label = cultura.TextInfo.ToTitleCase(month.ToString("MMM", cultura)).TrimEnd('.');
                data.Add(new MonthlyPurchaseResponseDto(label, amount));
            }

            return Results.Ok(new ApiResponse<List<MonthlyPurchaseResponseDto>>
            {
                Success = true,
                Message = "Compras por mes.",
                StatusCode = StatusCodes.Status200OK,
                Data = data
            });
        });
    }
}
