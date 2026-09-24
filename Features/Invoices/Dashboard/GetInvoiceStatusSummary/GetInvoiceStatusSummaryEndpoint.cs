using Microsoft.EntityFrameworkCore;

public static class GetInvoiceStatusSummaryEndpoint
{
    public static void MapGetInvoiceStatusSummary(this IEndpointRouteBuilder app)
    {
        app.MapGet("/status-summary", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var counts = await db.Invoices
                .AsNoTracking()
                .Where(x => x.IssueDate >= monthStart && x.IssueDate < monthEnd)
                .GroupBy(x => x.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var response = Enum.GetValues<InvoiceStatus>()
                .Select(status => new InvoiceStatusSummaryResponseDto(
                    status.ToString(),
                    counts.FirstOrDefault(c => c.Status == status)?.Count ?? 0))
                .ToList();

            return Results.Ok(new ApiResponse<List<InvoiceStatusSummaryResponseDto>>
            {
                Success = true,
                Message = "Facturas por estado.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
