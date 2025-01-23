public static class LoginUserEndpoints
{
    public static void MapLogin(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api");
        group.MapLoginUser();
        
    }
}