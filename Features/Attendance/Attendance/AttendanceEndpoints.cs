public static class AttendanceEndpoints
{
    public static RouteGroupBuilder MapAttendance(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/attendance").WithTags("HR - Attendance").RequirePermission(PermissionCodes.Employees);

        group.MapRegisterAttendance();
        group.MapGetAttendances();

        return group;
    }
}
