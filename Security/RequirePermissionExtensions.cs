public static class RequirePermissionExtensions
{
    public static RouteGroupBuilder RequirePermission(this RouteGroupBuilder builder, string permissionCode)
    {
        builder.RequireAuthorization(permissionCode);
        return builder;
    }

    public static RouteHandlerBuilder RequirePermission(this RouteHandlerBuilder builder, string permissionCode)
    {
        builder.RequireAuthorization(permissionCode);
        return builder;
    }
}
