using Microsoft.EntityFrameworkCore;

public static class GetTopCustomersEndpoint
{
    public static void MapGetTopCustomers(this IEndpointRouteBuilder app)
    {
        app.MapGet("/top-customers", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var topTotals = await db.Invoices
                .AsNoTracking()
                .Where(x => x.IssueDate >= monthStart && x.IssueDate < monthEnd && x.Status != InvoiceStatus.Cancelled)
                .GroupBy(x => x.CustomerId)
                .Select(g => new { CustomerId = g.Key, TotalInvoiced = g.Sum(x => x.Total) })
                .OrderByDescending(x => x.TotalInvoiced)
                .Take(10)
                .ToListAsync();

            var customerIds = topTotals.Select(x => x.CustomerId).ToList();
            var customerNames = await db.Customers
                .AsNoTracking()
                .Where(x => customerIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => (x.FirtsName + " " + (x.LastName ?? "")).Trim());

            var response = topTotals
                .Select(x => new TopCustomerResponseDto(
                    x.CustomerId,
                    customerNames.TryGetValue(x.CustomerId, out var name) ? name : string.Empty,
                    x.TotalInvoiced))
                .ToList();

            return Results.Ok(new ApiResponse<List<TopCustomerResponseDto>>
            {
                Success = true,
                Message = "Top clientes del mes.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
