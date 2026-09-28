public static class ExpenseDashboardEndpoints
{
    public static void MapExpenseDashboard(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/expense-dashboard").WithTags("ExpenseDashboard").RequirePermission(PermissionCodes.Expenses);

        group.MapGetExpenseDashboardSummary();
        group.MapGetExpensesByCategory();
        group.MapGetExpensesMonthlyTrend();
        group.MapGetExpensesByBranch();
        group.MapGetRecentExpenses();
        group.MapGetTopExpenses();
    }
}
