public static class DeletePositionEndpoint
{
    public static RouteGroupBuilder MapDeletePosition(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (int id, IDeletePositionService service) =>
        {
            var deleted = await service.DeletePositionAsync(id);
            if (!deleted)
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Cargo con ID {id} no encontrado."
                });
            }

            return Results.Ok(new ApiResponse<string>
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Cargo desactivado correctamente"
            });
        })
        .WithName("DeletePosition")
        .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
