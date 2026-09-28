public static class BranchSalesEndpoints
{
    public static void MapBranchSales(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/branchSales").RequirePermission(PermissionCodes.Settings);
        group.MapGetBranchSales();
        group.MapGetBranchesSales();
        group.MapCreateBranchSales();
        group.MapUpdateBranchSales();
        group.MapDeleteBranchSales();
    }
}