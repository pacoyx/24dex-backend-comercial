public static class SneakerEndpoints
{
    public static void MapSneakers(this IEndpointRouteBuilder app)
    {
        var workGuideDetailGroup = app.MapGroup("/api/workguide-details").WithTags("Sneakers").RequirePermission(PermissionCodes.Reception);
        workGuideDetailGroup.MapUpsertSneakerDetail();
        workGuideDetailGroup.MapGetSneakerDetail();

        var brandGroup = app.MapGroup("/api/sneaker-brands").WithTags("SneakerBrands").RequirePermission(PermissionCodes.Reception);
        brandGroup.MapGetSneakerBrands();
    }
}
