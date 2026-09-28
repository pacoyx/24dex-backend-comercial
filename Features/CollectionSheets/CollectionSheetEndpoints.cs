public static class CollectionSheetEndpoints
{
    public static void MapCollectionSheets(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/collection-sheets").WithTags("CollectionSheets").RequirePermission(PermissionCodes.Invoices);
        group.MapCreateCollectionSheet();
        group.MapGetCollectionSheet();
        group.MapGetCollectionSheets();
        group.MapAddPaymentToCollectionSheet();
        group.MapGetCollectionSheetPayments();
        group.MapCloseCollectionSheet();
    }
}
