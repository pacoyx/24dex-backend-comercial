public static class UserRolesEndpoints
{
    public static void MapUserRoles(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").WithTags("UserRoles").RequirePermission(PermissionCodes.Settings);

        group.MapGetUserRoles();
        group.MapUpdateUserRoles();
    }
}
