public static class CollectionWorkerEndpoint
{
    public static void MapClothingWorkerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clothingWorker");
        group.MapGetCollectionWorkerById();
    }
}