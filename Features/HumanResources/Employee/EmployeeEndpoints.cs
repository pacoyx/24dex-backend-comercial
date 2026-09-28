public static class EmployeeEndpoints
{
    public static RouteGroupBuilder MapEmployee(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employees").WithTags("HR - Employees").RequirePermission(PermissionCodes.Employees);

        group.MapCreateEmployee();
        group.MapGetEmployees();
        group.MapGetEmployee();
        group.MapUpdateEmployee();
        group.MapTerminateEmployee();

        return group;
    }
}
