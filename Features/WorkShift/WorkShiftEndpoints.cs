public static class WorkShiftEndpoints{
    public static void MapWorkShift(this WebApplication app){
        var group = app.MapGroup("/api/workShifts");
        group.MapCreateWorkShift();
        group.MapGetWorkShift();
        group.MapGetWorkShifts();
        group.MapDeleteWorkShift();
    }
}