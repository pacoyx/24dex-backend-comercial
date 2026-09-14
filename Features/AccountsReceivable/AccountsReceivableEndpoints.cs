public static class AccountsReceivableEndpoints
{
    public static void MapAccountsReceivable(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accounts-receivable").WithTags("AccountsReceivable");
        group.MapGetAccountsReceivable();
    }
}
