public static class HrReportsEndpoints
{
    public static RouteGroupBuilder MapHrReports(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/hr/reports").WithTags("HR - Reports").RequirePermission(PermissionCodes.Employees);

        group.MapGetPayrollReport();
        group.MapGetLoansReport();
        group.MapGetAdvancesReport();
        group.MapGetAttendanceReport();
        group.MapGetLateArrivalsReport();
        group.MapGetExtraHoursReport();

        return group;
    }
}
