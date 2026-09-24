using Microsoft.EntityFrameworkCore;

public static class GetRecentInvoicesEndpoint
{
    public static void MapGetRecentInvoices(this IEndpointRouteBuilder app)
    {
        app.MapGet("/recent-invoices", async (RecepcionDbContext db) =>
        {
            var recentInvoices = await db.Invoices
                .AsNoTracking()
                .Include(x => x.Customer)
                .OrderByDescending(x => x.IssueDate)
                .Take(20)
                .Select(x => new
                {
                    x.Id,
                    x.IssueDate,
                    CustomerName = (x.Customer.FirtsName + " " + (x.Customer.LastName ?? "")).Trim(),
                    DocumentNumber = x.Serie + "-" + x.Numero,
                    x.Total,
                    x.Status
                })
                .ToListAsync();

            var response = recentInvoices
                .Select(x => new RecentInvoiceResponseDto(x.Id, x.IssueDate, x.CustomerName, x.DocumentNumber, x.Total, x.Status.ToString()))
                .ToList();

            return Results.Ok(new ApiResponse<List<RecentInvoiceResponseDto>>
            {
                Success = true,
                Message = "Facturas recientes.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
