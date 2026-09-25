public static class RegisterPermissionEndpoint
{
    public static RouteGroupBuilder MapRegisterPermission(this RouteGroupBuilder group)
    {
        group.MapPost("/{employeeId:long}/permissions", async (long employeeId, IRegisterIncidentService service, RegisterIncidentRequestDto request) =>
        {
            var result = await service.RegisterIncidentAsync(employeeId, AttendanceIncidentType.Permission, request);
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
                Message = "Permiso registrado correctamente"
            });
        })
        .WithName("RegisterPermission")
        .Produces<ApiResponse<AttendanceIncidentResponseDto>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
