public static class RegisterAttendanceEndpoint
{
    public static RouteGroupBuilder MapRegisterAttendance(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (IRegisterAttendanceService service, RegisterAttendanceRequestDto request) =>
        {
            var result = await service.RegisterAttendanceAsync(request);
            return Results.Ok(new ApiResponse<AttendanceResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Asistencia registrada correctamente"
            });
        })
        .WithName("RegisterAttendance")
        .Produces<ApiResponse<AttendanceResponseDto>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
