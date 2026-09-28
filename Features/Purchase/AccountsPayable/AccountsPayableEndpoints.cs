public static class AccountsPayableEndpoints
{
    public static void MapAccountsPayable(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accounts-payable").WithTags("AccountsPayable").RequirePermission(PermissionCodes.Purchases);
        group.MapGetAccountsPayable();
    }
}
