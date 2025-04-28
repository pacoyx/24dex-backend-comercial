public static class PurchaseReportsEndpoints
{
    public static RouteGroupBuilder MapPurchaseReports(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchase/reports");

        group.MapGetInvoicesByParamsEndpoint();        

        return group;
    }
}    