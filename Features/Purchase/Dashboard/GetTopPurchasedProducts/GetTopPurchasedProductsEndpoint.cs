using Microsoft.EntityFrameworkCore;

public static class GetTopPurchasedProductsEndpoint
{
    public static void MapGetTopPurchasedProducts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/top-products", async (RecepcionDbContext db) =>
        {
            var topQuantities = await db.PurchaseInvoiceDetails
                .AsNoTracking()
                .Where(x => x.PurchaseInvoice != null && x.PurchaseInvoice.PaymentStatus != PurchaseInvoiceStatus.Cancelled)
                .GroupBy(x => x.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.Quantity)
                .Take(10)
                .ToListAsync();

            var productIds = topQuantities.Select(x => x.ProductId).ToList();
            var productNames = await db.Products
                .AsNoTracking()
                .Where(x => productIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.Name);

            var topProducts = topQuantities
                .Select(x => new TopPurchasedProductResponseDto(
                    productNames.TryGetValue(x.ProductId, out var name) ? name : string.Empty,
                    x.Quantity))
                .ToList();

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
