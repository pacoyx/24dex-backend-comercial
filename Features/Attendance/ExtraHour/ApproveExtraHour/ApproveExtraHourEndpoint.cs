public static class ApproveExtraHourEndpoint
{
    public static RouteGroupBuilder MapApproveExtraHour(this RouteGroupBuilder group)
    {
        group.MapPost("/{id:long}/approve", async (long id, IApproveExtraHourService service) =>
        {
            var result = await service.ApproveExtraHourAsync(id);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<ExtraHourResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Registro de horas extras con ID {id} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<ExtraHourResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Horas extras aprobadas correctamente"
            });
        })
        .WithName("ApproveExtraHour")
        .Produces<ApiResponse<ExtraHourResponseDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
