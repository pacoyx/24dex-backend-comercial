public static class AttendanceEndpoints
{
    public static RouteGroupBuilder MapAttendance(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/attendance").WithTags("HR - Attendance");

        group.MapRegisterAttendance();
        group.MapGetAttendances();

        return group;
    }
}
