public static class RegisterExtraHourEndpoint
{
    public static RouteGroupBuilder MapRegisterExtraHour(this RouteGroupBuilder group)
    {
        group.MapPost("/{employeeId:long}/extra-hours", async (long employeeId, IRegisterExtraHourService service, RegisterExtraHourRequestDto request) =>
        {
            var result = await service.RegisterExtraHourAsync(employeeId, request);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<ExtraHourResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Trabajador con ID {employeeId} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<ExtraHourResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Horas extras registradas correctamente"
            });
        })
        .WithName("RegisterExtraHour")
        .Produces<ApiResponse<ExtraHourResponseDto>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
