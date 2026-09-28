public static class PermissionsEndpoints
{
    public static void MapPermissions(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/permissions").WithTags("Permissions").RequirePermission(PermissionCodes.Settings);

        group.MapGetPermissions();
    }
}
