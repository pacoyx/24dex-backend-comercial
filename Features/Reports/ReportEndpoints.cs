public static class ReportEndpoints
{
    public static void MapReport(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reports").RequirePermission(PermissionCodes.Reports);
        group.MapWorkGuideByDate();
        group.MapWorkGuideByCustomers();
        group.MapDashboardCash();
        group.MapDashboardCashWeek();
        group.MapDashboardCashMonth();
        group.MapSalesRegister();
        group.MapPurchaseRegister();
        group.MapSneakerReport();
    }
}