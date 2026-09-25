public static class GetAttendanceReportEndpoint
{
    public static RouteGroupBuilder MapGetAttendanceReport(this RouteGroupBuilder group)
    {
        group.MapGet("/attendance", async (IGetAttendancesService service, long? employeeId, DateOnly? startDate, DateOnly? endDate) =>
        {
            var result = await service.GetAttendancesAsync(employeeId, startDate, endDate);
            return Results.Ok(new ApiResponse<IEnumerable<AttendanceListItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Reporte de asistencia obtenido correctamente"
            });
        })
        .WithName("GetAttendanceReport")
        .Produces<ApiResponse<IEnumerable<AttendanceListItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
