public static class PermissionCodes
{
    public const string Dashboard = "dashboard.access";
    public const string Reception = "reception.access";
    public const string Customers = "customers.access";
    public const string Invoices = "invoices.access";
    public const string Purchases = "purchases.access";
    public const string Expenses = "expenses.access";
    public const string Employees = "employees.access";
    public const string Payroll = "payroll.access";
    public const string Suppliers = "suppliers.access";
    public const string Reports = "reports.access";
    public const string Settings = "settings.access";
    public const string CorporateOperations = "corporate-operations.access";
    public const string PickupMobile = "pickup_mobile.access";

    public static readonly string[] All =
    {
        Dashboard, Reception, Customers, Invoices, Purchases,
        Expenses, Employees, Payroll, Suppliers, Reports, Settings,
        CorporateOperations, PickupMobile
    };
}
