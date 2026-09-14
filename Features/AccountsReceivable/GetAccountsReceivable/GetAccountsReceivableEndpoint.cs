using Microsoft.EntityFrameworkCore;

public static class GetAccountsReceivableEndpoint
{
    public static void MapGetAccountsReceivable(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;

            var accountsReceivable = await db.Invoices
                .AsNoTracking()
                .Include(x => x.Customer)
                .Where(x => x.Status == InvoiceStatus.Issued || x.Status == InvoiceStatus.PartiallyPaid)
                .OrderBy(x => x.DueDate)
                .Select(x => new AccountReceivableResponse(
                    x.CustomerId,
                    (x.Customer.FirtsName + " " + (x.Customer.LastName ?? "")).Trim(),
                    x.Id,
                    x.Serie,
                    x.Numero,
                    x.Total,
                    x.AmountPaid,
                    x.Balance,
                    x.Status,
                    EF.Functions.DateDiffDay(x.DueDate, today)
                ))
                .ToListAsync();

            var response = accountsReceivable
                .Select(x => x with { OverdueDays = Math.Max(0, x.OverdueDays) })
                .ToList();

            return Results.Ok(new ApiResponse<List<AccountReceivableResponse>>
            {
                Success = true,
                Message = "Cuentas por cobrar pendientes.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
