public static class TerminateEmployeeEndpoint
{
    public static RouteGroupBuilder MapTerminateEmployee(this RouteGroupBuilder group)
    {
        group.MapPost("/{id:long}/terminate", async (long id, ITerminateEmployeeService service, TerminateEmployeeRequestDto request) =>
        {
            var result = await service.TerminateEmployeeAsync(id, request);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<EmployeeResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Trabajador con ID {id} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<EmployeeResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Trabajador dado de baja correctamente"
            });
        })
        .WithName("TerminateEmployee")
        .Produces<ApiResponse<EmployeeResponseDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
