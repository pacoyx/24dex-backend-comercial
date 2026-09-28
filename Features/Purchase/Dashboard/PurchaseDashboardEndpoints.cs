public static class PurchaseDashboardEndpoints
{
    public static void MapPurchaseDashboard(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/purchase-dashboard").WithTags("PurchaseDashboard").RequirePermission(PermissionCodes.Purchases);
        group.MapGetPurchaseDashboardSummary();
        group.MapGetMonthlyPurchases();
        group.MapGetTopPurchasedProducts();
        group.MapGetRecentPurchases();
        group.MapGetAccountsPayableSummary();
    }
}
