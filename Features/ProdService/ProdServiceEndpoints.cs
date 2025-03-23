public static class ProdServiceEndpoints{
    public static RouteGroupBuilder MapProdService(this IEndpointRouteBuilder app){
        var group = app.MapGroup("/api/prodService");
        
        
        // group.CacheOutput();

        group.MapCreateProdService();
        group.MapGetProdService();
        group.MapGetProdServices();
        group.MapDeleteProdService();
        group.MapUpdateProdService();
        group.MapGetProdServiceByCat();
        group.MapGetProdServiceSearchByDescription();
        group.MapGetIdServicePeso();
        group.MapGetServicesAccessFast();
    


        return group;
    }
}