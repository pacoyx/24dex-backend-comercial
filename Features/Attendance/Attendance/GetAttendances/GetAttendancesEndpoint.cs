public static class GetAttendancesEndpoint
{
    public static RouteGroupBuilder MapGetAttendances(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetAttendancesService service, long? employeeId, DateOnly? startDate, DateOnly? endDate) =>
        {
            var result = await service.GetAttendancesAsync(employeeId, startDate, endDate);
            return Results.Ok(new ApiResponse<IEnumerable<AttendanceListItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Asistencias obtenidas correctamente"
            });
        })
        .WithName("GetAttendances")
        .Produces<ApiResponse<IEnumerable<AttendanceListItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
