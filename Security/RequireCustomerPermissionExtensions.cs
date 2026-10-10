public static class RequireCustomerPermissionExtensions
{
    public const string PolicyPrefix = "customer:";

    public static RouteGroupBuilder RequireCustomerPermission(this RouteGroupBuilder builder, string permissionCode)
    {
        builder.RequireAuthorization(PolicyPrefix + permissionCode);
        return builder;
    }

    public static RouteHandlerBuilder RequireCustomerPermission(this RouteHandlerBuilder builder, string permissionCode)
    {
        builder.RequireAuthorization(PolicyPrefix + permissionCode);
        return builder;
    }
}
