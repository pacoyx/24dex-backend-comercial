public static class PurchaseReportsEndpoints
{
    public static RouteGroupBuilder MapPurchaseReports(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchase/reports").RequirePermission(PermissionCodes.Purchases);

        group.MapGetInvoicesByParamsEndpoint();        

        return group;
    }
}    