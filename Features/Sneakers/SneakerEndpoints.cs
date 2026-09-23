public static class SneakerEndpoints
{
    public static void MapSneakers(this IEndpointRouteBuilder app)
    {
        var workGuideDetailGroup = app.MapGroup("/api/workguide-details").WithTags("Sneakers");
        workGuideDetailGroup.MapUpsertSneakerDetail();
        workGuideDetailGroup.MapGetSneakerDetail();

        var brandGroup = app.MapGroup("/api/sneaker-brands").WithTags("SneakerBrands");
        brandGroup.MapGetSneakerBrands();
    }
}
