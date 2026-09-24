using Microsoft.EntityFrameworkCore;

public static class GetServiceSalesEndpoint
{
    public static void MapGetServiceSales(this IEndpointRouteBuilder app)
    {
        app.MapGet("/service-sales", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var totalsByService = await db.InvoiceDetails
                .AsNoTracking()
                .Where(x => x.Invoice.IssueDate >= monthStart
                    && x.Invoice.IssueDate < monthEnd
                    && x.Invoice.Status != InvoiceStatus.Cancelled)
                .GroupBy(x => x.Description)
                .Select(g => new { ServiceName = g.Key, Amount = g.Sum(x => x.Total) })
                .OrderByDescending(x => x.Amount)
                .ToListAsync();

            var response = totalsByService
                .Select(x => new ServiceSalesResponseDto(x.ServiceName, x.Amount))
                .ToList();

            return Results.Ok(new ApiResponse<List<ServiceSalesResponseDto>>
            {
                Success = true,
                Message = "Facturación por servicio.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
