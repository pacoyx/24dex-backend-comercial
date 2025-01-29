public static class UserEndpoints
{
    public static void MapUser(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/user"); 
        group.MapCreateUser(); 
        group.MapUpdateUser();
        group.MapDeleteUser();
        group.MapGetUsers();
        group.MapGetUser();
    }
}