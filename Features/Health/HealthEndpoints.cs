public static class HealthEndpoints{
    public static void MapHealth(this IEndpointRouteBuilder app){
        var group = app.MapGroup("/api/health");
        group.MapGetHealth();
    }
}