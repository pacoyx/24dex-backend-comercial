using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetCustomerSalesEndpoint
{
    public static void MapGetCustomerSales(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customer-sales", async (
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            RecepcionDbContext db) =>
        {
            var query = db.Invoices
                .AsNoTracking()
                .Where(x => x.Status != InvoiceStatus.Cancelled)
                .AsQueryable();

            if (dateFrom.HasValue)
            {
                query = query.Where(x => x.IssueDate.Date >= dateFrom.Value.Date);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(x => x.IssueDate.Date <= dateTo.Value.Date);
            }

            var totalsByCustomer = await query
                .GroupBy(x => x.CustomerId)
                .Select(g => new { CustomerId = g.Key, Amount = g.Sum(x => x.Total) })
                .OrderByDescending(x => x.Amount)
                .ToListAsync();

            var customerIds = totalsByCustomer.Select(x => x.CustomerId).ToList();
            var customerNames = await db.Customers
                .AsNoTracking()
                .Include(x => x.BillingProfile)
                .Where(x => customerIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.CustomerType == CustomerType.Company && x.BillingProfile != null
                    ? x.BillingProfile.BusinessName
                    : (x.FirtsName + " " + (x.LastName ?? "")).Trim());

            var response = totalsByCustomer
                .Select(x => new CustomerSalesResponseDto(
                    customerNames.TryGetValue(x.CustomerId, out var name) ? name : string.Empty,
                    x.Amount))
                .ToList();

            return Results.Ok(new ApiResponse<List<CustomerSalesResponseDto>>
            {
                Success = true,
                Message = "Facturación por cliente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
