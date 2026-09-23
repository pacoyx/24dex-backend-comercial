using Microsoft.EntityFrameworkCore;

public static class GetTopPurchasedProductsEndpoint
{
    public static void MapGetTopPurchasedProducts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/top-products", async (RecepcionDbContext db) =>
        {
            var topProducts = await db.PurchaseInvoiceDetails
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.PurchaseInvoice)
                .Where(x => x.PurchaseInvoice != null && x.PurchaseInvoice.PaymentStatus != PurchaseInvoiceStatus.Cancelled)
                .GroupBy(x => new { x.ProductId, ProductName = x.Product != null ? x.Product.Name : string.Empty })
                .Select(g => new TopPurchasedProductResponseDto(g.Key.ProductName, g.Sum(x => x.Quantity)))
                .OrderByDescending(x => x.Quantity)
                .Take(10)
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<TopPurchasedProductResponseDto>>
            {
                Success = true,
                Message = "Top productos comprados.",
                StatusCode = StatusCodes.Status200OK,
                Data = topProducts
            });
        });
    }
}
