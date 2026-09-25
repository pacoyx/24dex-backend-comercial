public static class ExtraHourEndpoints
{
    public static void MapExtraHour(this IEndpointRouteBuilder app)
    {
        var employeesGroup = app.MapGroup("/api/employees").WithTags("HR - ExtraHours");
        employeesGroup.MapRegisterExtraHour();

        var extraHoursGroup = app.MapGroup("/api/extra-hours").WithTags("HR - ExtraHours");
        extraHoursGroup.MapGetExtraHours();
        extraHoursGroup.MapApproveExtraHour();
    }
}
