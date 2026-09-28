public static class UserEndpoints
{
    public static void MapUser(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/user");

        var adminGroup = group.MapGroup("").RequirePermission(PermissionCodes.Settings);
        adminGroup.MapCreateUser();
        adminGroup.MapUpdateUser();
        adminGroup.MapDeleteUser();
        adminGroup.MapGetUsers();
        adminGroup.MapGetUser();

        group.MapMyProfile();
    }
}
