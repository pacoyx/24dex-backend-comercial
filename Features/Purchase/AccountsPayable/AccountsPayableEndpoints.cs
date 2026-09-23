public static class AccountsPayableEndpoints
{
    public static void MapAccountsPayable(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accounts-payable").WithTags("AccountsPayable");
        group.MapGetAccountsPayable();
    }
}
