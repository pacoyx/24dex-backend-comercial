public static class AreaEndpoints
{
    public static RouteGroupBuilder MapArea(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/hr/areas").WithTags("HR - Areas").RequirePermission(PermissionCodes.Employees);

        group.MapCreateArea();
        group.MapGetAreas();
        group.MapUpdateArea();
        group.MapDeleteArea();

        return group;
    }
}
