public static class HrDashboardEndpoints
{
    public static void MapHrDashboard(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("").RequirePermission(PermissionCodes.Employees);
        group.MapGetHrDashboardSummary();
        group.MapGetEmployeesByArea();
        group.MapGetPendingLoans();
        group.MapGetUpcomingBirthdays();
    }
}
