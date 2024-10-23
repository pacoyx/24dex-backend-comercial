public static class LoginUserEndpoints
{
    public static void MapLoginUser(this WebApplication app)
    {
        var group = app.MapGroup("/api/login");
        group.MapLoginUser();
        
    }
}