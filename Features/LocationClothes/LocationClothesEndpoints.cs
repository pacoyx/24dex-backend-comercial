public static class LocationClothesEndpoints
{
    public static void MapLocationClothes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/locationclothes").RequirePermission(PermissionCodes.Reception);
        group.MapCreateLocation();
        group.MapGetLocations();
        group.MapRegisterLocationWorkGuide();
        group.MapUpdateLocation();
        group.MapDeleteLocation();
    }
}