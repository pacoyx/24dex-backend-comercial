using Microsoft.EntityFrameworkCore;

public static class GetRecentPurchasesEndpoint
{
    public static void MapGetRecentPurchases(this IEndpointRouteBuilder app)
    {
        app.MapGet("/recent-purchases", async (RecepcionDbContext db) =>
        {
            var recentPurchases = await db.PurchaseInvoices
                .AsNoTracking()
                .Include(x => x.Supplier)
                .OrderByDescending(x => x.InvoiceIssueDate)
                .Take(10)
                .Select(x => new RecentPurchaseResponseDto(
                    x.Id,
                    x.Supplier != null ? x.Supplier.Name : string.Empty,
                    x.InvoiceSerie + "-" + x.InvoiceNumber,
                    x.InvoiceIssueDate,
                    x.Total,
                    x.PaymentStatus
                ))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<RecentPurchaseResponseDto>>
            {
                Success = true,
                Message = "Compras recientes.",
                StatusCode = StatusCodes.Status200OK,
                Data = recentPurchases
            });
        });
    }
}
