public static class InvoiceDashboardEndpoints
{
    public static void MapInvoiceDashboard(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/invoice-dashboard").WithTags("InvoiceDashboard");
        group.MapGetInvoiceDashboardSummary();
        group.MapGetMonthlySales();
        group.MapGetInvoiceStatusSummary();
        group.MapGetDashboardAccountsReceivable();
        group.MapGetOverdueInvoices();
        group.MapGetTopCustomers();
        group.MapGetRecentInvoices();
        group.MapGetCustomerSales();
        group.MapGetInvoiceBranchSales();
        group.MapGetServiceSales();
    }
}
