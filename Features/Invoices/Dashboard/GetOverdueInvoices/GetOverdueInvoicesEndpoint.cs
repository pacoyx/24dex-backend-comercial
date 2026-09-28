using Microsoft.EntityFrameworkCore;

public static class GetOverdueInvoicesEndpoint
{
    public static void MapGetOverdueInvoices(this IEndpointRouteBuilder app)
    {
        app.MapGet("/overdue-invoices", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;

            var overdueInvoices = await db.Invoices
                .AsNoTracking()
                .Include(x => x.Customer)
                .Where(x => x.DueDate < today && x.Balance > 0 && x.Status != InvoiceStatus.Cancelled)
                .Select(x => new
                {
                    x.Id,
                    DocumentNumber = x.Serie + "-" + x.Numero,
                    CustomerName = x.Customer.CustomerType == CustomerType.Company && x.Customer.BillingProfile != null
                        ? x.Customer.BillingProfile.BusinessName
                        : (x.Customer.FirtsName + " " + (x.Customer.LastName ?? "")).Trim(),
                    x.Balance,
                    DaysPastDue = EF.Functions.DateDiffDay(x.DueDate, today)
                })
                .OrderByDescending(x => x.DaysPastDue)
                .ToListAsync();

            var response = overdueInvoices
                .Select(x => new OverdueInvoiceResponseDto(x.Id, x.DocumentNumber, x.CustomerName, x.Balance, x.DaysPastDue))
                .ToList();

            return Results.Ok(new ApiResponse<List<OverdueInvoiceResponseDto>>
            {
                Success = true,
                Message = "Facturas vencidas.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
