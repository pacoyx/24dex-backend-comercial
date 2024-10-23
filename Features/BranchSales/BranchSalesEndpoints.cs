public static class BranchSalesEndpoints
{
    public static void MapBranchSales(this WebApplication app)
    {
        var group = app.MapGroup("/api/branchSales");
        group.MapGetBranchSales();
        group.MapGetBranchesSales();
        group.MapCreateBranchSales();
    }
}