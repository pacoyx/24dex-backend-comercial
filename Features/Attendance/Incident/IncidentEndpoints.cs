public static class IncidentEndpoints
{
    public static void MapIncident(this IEndpointRouteBuilder app)
    {
        var employeesGroup = app.MapGroup("/api/employees").WithTags("HR - Attendance").RequirePermission(PermissionCodes.Employees);
        employeesGroup.MapRegisterAbsence();
        employeesGroup.MapRegisterLateArrival();
        employeesGroup.MapRegisterPermission();

        var incidentsGroup = app.MapGroup("/api/attendance/incidents").WithTags("HR - Attendance").RequirePermission(PermissionCodes.Employees);
        incidentsGroup.MapGetIncidents();
    }
}
