public static class RegisterAbsenceEndpoint
{
    public static RouteGroupBuilder MapRegisterAbsence(this RouteGroupBuilder group)
    {
        group.MapPost("/{employeeId:long}/absences", async (long employeeId, IRegisterIncidentService service, RegisterIncidentRequestDto request) =>
        {
            var result = await service.RegisterIncidentAsync(employeeId, AttendanceIncidentType.Absence, request);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<AttendanceIncidentResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Trabajador con ID {employeeId} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<AttendanceIncidentResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Falta registrada correctamente"
            });
        })
        .WithName("RegisterAbsence")
        .Produces<ApiResponse<AttendanceIncidentResponseDto>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
