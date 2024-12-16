public static class CustomerEndpoints
{
    public static void MapCustomer(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customer");
        group.MapGetCustomer();
        group.MapGetCustomers();
        group.MapCreateCustomer();
        group.MapUpdateCustomer();
        group.MapDeleteCustomer();
        group.MapSearchCustomer();
        group.MapUpdateCustomerPhone();
        group.MapSearchCustomerPaginator();
    }
}