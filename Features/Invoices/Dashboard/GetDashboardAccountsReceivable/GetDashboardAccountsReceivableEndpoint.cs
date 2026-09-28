using Microsoft.EntityFrameworkCore;

public static class GetDashboardAccountsReceivableEndpoint
{
    public static void MapGetDashboardAccountsReceivable(this IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts-receivable", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;

            var accountsReceivable = await db.Invoices
                .AsNoTracking()
                .Include(x => x.Customer)
                .Where(x => x.Status == InvoiceStatus.Issued || x.Status == InvoiceStatus.PartiallyPaid)
                .OrderByDescending(x => x.Balance)
                .Take(20)
                .Select(x => new
                {
                    x.Id,
                    CustomerName = x.Customer.CustomerType == CustomerType.Company && x.Customer.BillingProfile != null
                        ? x.Customer.BillingProfile.BusinessName
                        : (x.Customer.FirtsName + " " + (x.Customer.LastName ?? "")).Trim(),
                    DocumentNumber = x.Serie + "-" + x.Numero,
                    x.Total,
                    x.AmountPaid,
                    x.Balance,
                    x.Status,
                    DaysPastDue = EF.Functions.DateDiffDay(x.DueDate, today)
                })
                .ToListAsync();

            var response = accountsReceivable
                .Select(x => new DashboardAccountReceivableResponseDto(
                    x.Id, x.CustomerName, x.DocumentNumber, x.Total, x.AmountPaid, x.Balance,
                    x.Status.ToString(), Math.Max(0, x.DaysPastDue)))
                .ToList();

            return Results.Ok(new ApiResponse<List<DashboardAccountReceivableResponseDto>>
            {
                Success = true,
                Message = "Cuentas por cobrar pendientes.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
