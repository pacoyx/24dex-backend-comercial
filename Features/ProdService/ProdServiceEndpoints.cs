public static class ProdServiceEndpoints{
    public static void MapProdService(this IEndpointRouteBuilder app){
        var group = app.MapGroup("/api/prodService");
        group.MapCreateProdService();
        group.MapGetProdService();
        group.MapGetProdServices();
        group.MapDeleteProdService();
        group.MapUpdateProdService();
        group.MapGetProdServiceByCat();
        group.MapGetProdServiceSearchByDescription();
        group.MapGetIdServicePeso();
    }
}