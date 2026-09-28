public static class CatServiceEndpoints{
    public static void MapCatService(this IEndpointRouteBuilder app){
        var group = app.MapGroup("/api/catService").RequirePermission(PermissionCodes.Reception);
        group.MapCreateCatService();
        group.MapGetCatService();
        group.MapCatServices();
        group.MapDeleteCat();
        group.MapUpdateCatService();        
    }
}