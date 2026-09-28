public static class WorkShiftEndpoints{
    public static void MapWorkShift(this IEndpointRouteBuilder app){
        var group = app.MapGroup("/api/workShifts").RequirePermission(PermissionCodes.Settings);
        group.MapCreateWorkShift();
        group.MapGetWorkShift();
        group.MapGetWorkShifts();
        group.MapDeleteWorkShift();
    }
}