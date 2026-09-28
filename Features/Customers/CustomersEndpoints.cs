public static class CustomersEndpoints
{
    public static void MapCustomers(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customers").WithTags("Customers").RequirePermission(PermissionCodes.Customers);
        group.MapCompanySearch();
        group.MapBillingProfiles();
        group.MapCustomerContacts();
        group.MapAccountSummary();

        var contactsGroup = app.MapGroup("/api/customer-contacts").WithTags("Customers").RequirePermission(PermissionCodes.Customers);
        contactsGroup.MapCustomerContactCommands();
    }
}
