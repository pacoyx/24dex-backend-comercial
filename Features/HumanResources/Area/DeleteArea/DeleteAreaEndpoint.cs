public static class DeleteAreaEndpoint
{
    public static RouteGroupBuilder MapDeleteArea(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (int id, IDeleteAreaService service) =>
        {
            var deleted = await service.DeleteAreaAsync(id);
            if (!deleted)
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Area con ID {id} no encontrada."
                });
            }

            return Results.Ok(new ApiResponse<string>
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Area desactivada correctamente"
            });
        })
        .WithName("DeleteArea")
        .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return group;
    }
}
