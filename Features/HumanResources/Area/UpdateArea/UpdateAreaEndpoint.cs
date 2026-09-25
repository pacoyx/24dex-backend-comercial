public static class UpdateAreaEndpoint
{
    public static RouteGroupBuilder MapUpdateArea(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:int}", async (int id, IUpdateAreaService service, UpdateAreaRequestDto request) =>
        {
            var result = await service.UpdateAreaAsync(id, request);
            if (result == null)
            {
                return Results.NotFound(new ApiResponse<UpdateAreaResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Area con ID {id} no encontrada."
                });
            }

            return Results.Ok(new ApiResponse<UpdateAreaResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Area actualizada correctamente"
            });
        })
        .WithName("UpdateArea")
        .Produces<ApiResponse<UpdateAreaResponseDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
