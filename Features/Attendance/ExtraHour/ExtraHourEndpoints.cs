public static class ExtraHourEndpoints
{
    public static void MapExtraHour(this IEndpointRouteBuilder app)
    {
        var employeesGroup = app.MapGroup("/api/employees").WithTags("HR - ExtraHours").RequirePermission(PermissionCodes.Employees);
        employeesGroup.MapRegisterExtraHour();

        var extraHoursGroup = app.MapGroup("/api/extra-hours").WithTags("HR - ExtraHours").RequirePermission(PermissionCodes.Employees);
        extraHoursGroup.MapGetExtraHours();
        extraHoursGroup.MapApproveExtraHour();
    }
}
