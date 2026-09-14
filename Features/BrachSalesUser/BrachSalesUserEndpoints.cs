public static class BrachSalesUserEndpoints
{
    public static void MapBrachSalesUser(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/brachSalesUser").WithTags("BrachSalesUser");
        group.MapGetBrachSalesUser();
        group.MapGetBrachSalesUsers();
        group.MapGetBrachSalesUsersByBranch();
        group.MapCreateBrachSalesUser();
        group.MapUpdateBrachSalesUser();
        group.MapDeleteBrachSalesUser();
    }
}
