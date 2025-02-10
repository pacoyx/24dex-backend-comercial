public static class ClothingItemEndpoint
{
    public static void MapClothingItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clothingItem");
        group.MapGetClothingItems();
    }
}