public static class PurchaseInvoiceEndpoints
{
    public static RouteGroupBuilder MapPurchaseInvoice(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchase-invoice").RequirePermission(PermissionCodes.Purchases);

 
        group.MapCreatePurchaseInvoice();
        group.MapGetPuchasesInvoice();
        group.MapRegisterPurchasePayment();
        group.MapGetPurchasePayments();
        group.MapCancelPurchaseInvoice();
        // group.MapUpdatePurchaseInvoice();
        // group.MapDeletePurchaseInvoice();

        return group;
    }
}