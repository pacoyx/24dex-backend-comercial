public static class CustomerEndpoints
{
    public static void MapCustomer(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customer").RequirePermission(PermissionCodes.Customers);
        group.MapGetCustomer();
        group.MapGetCustomers();
        group.MapCreateCustomer();
        group.MapUpdateCustomer();
        group.MapDeleteCustomer();
        group.MapSearchCustomer();
        group.MapSearchCustomerByPatron();
        group.MapUpdateCustomerPhone();
        group.MapSearchCustomerPaginator();
    }
}