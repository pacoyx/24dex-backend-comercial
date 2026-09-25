public static class UpdatePositionEndpoint
{
    public static RouteGroupBuilder MapUpdatePosition(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:int}", async (int id, IUpdatePositionService service, UpdatePositionRequestDto request) =>
        {
            var result = await service.UpdatePositionAsync(id, request);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<UpdatePositionResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Cargo con ID {id} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<UpdatePositionResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Cargo actualizado correctamente"
            });
        })
        .WithName("UpdatePosition")
        .Produces<ApiResponse<UpdatePositionResponseDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
