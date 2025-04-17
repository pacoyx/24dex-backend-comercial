public static class PurchaseInvoiceEndpoints
{
    public static RouteGroupBuilder MapPurchaseInvoice(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchase-invoice");

 
        group.MapCreatePurchaseInvoice();        
        group.MapGetPuchasesInvoice();
        // group.MapUpdatePurchaseInvoice();
        // group.MapDeletePurchaseInvoice();

        return group;
    }
}