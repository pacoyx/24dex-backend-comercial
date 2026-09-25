public static class GetEmployeeEndpoint
{
    public static RouteGroupBuilder MapGetEmployee(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:long}", async (long id, IGetEmployeeService service) =>
        {
            var result = await service.GetEmployeeAsync(id);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<EmployeeDetailResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Trabajador con ID {id} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<EmployeeDetailResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Trabajador obtenido correctamente"
            });
        })
        .WithName("GetEmployee")
        .Produces<ApiResponse<EmployeeDetailResponseDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
