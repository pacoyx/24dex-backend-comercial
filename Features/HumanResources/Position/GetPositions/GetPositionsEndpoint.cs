public static class GetPositionsEndpoint
{
    public static RouteGroupBuilder MapGetPositions(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetPositionsService service, bool? onlyActive) =>
        {
            var result = await service.GetPositionsAsync(onlyActive);
            var response = new ApiResponse<IEnumerable<GetPositionsResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Cargos obtenidos correctamente"
            };
            return Results.Ok(response);
        })
        .WithName("GetPositions")
        .Produces<ApiResponse<IEnumerable<GetPositionsResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
