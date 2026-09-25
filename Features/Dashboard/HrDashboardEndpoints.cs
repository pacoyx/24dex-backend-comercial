public static class HrDashboardEndpoints
{
    public static void MapHrDashboard(this IEndpointRouteBuilder app)
    {
        app.MapGetHrDashboardSummary();
        app.MapGetEmployeesByArea();
        app.MapGetPendingLoans();
        app.MapGetUpcomingBirthdays();
    }
}
