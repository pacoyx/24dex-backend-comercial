public static class GeneratePayrollEndpoint
{
    public static RouteGroupBuilder MapGeneratePayroll(this RouteGroupBuilder group)
    {
        group.MapPost("/{id:long}/generate", async (long id, IGeneratePayrollService service) =>
        {
            var result = await service.GeneratePayrollAsync(id);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<GeneratePayrollResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Periodo de planilla con ID {id} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<GeneratePayrollResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Planilla generada correctamente"
            });
        })
        .WithName("GeneratePayroll")
        .Produces<ApiResponse<GeneratePayrollResponseDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
