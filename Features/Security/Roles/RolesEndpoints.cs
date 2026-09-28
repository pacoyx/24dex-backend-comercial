public static class RolesEndpoints
{
    public static void MapRoles(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/roles").WithTags("Roles").RequirePermission(PermissionCodes.Settings);

        group.MapGetRoles();
        group.MapCreateRole();
        group.MapUpdateRole();
        group.MapDeleteRole();
        group.MapGetRolePermissions();
        group.MapUpdateRolePermissions();
    }
}
