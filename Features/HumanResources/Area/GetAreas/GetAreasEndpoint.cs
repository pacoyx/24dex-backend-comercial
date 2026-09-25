public static class GetAreasEndpoint
{
    public static RouteGroupBuilder MapGetAreas(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetAreasService service, bool? onlyActive) =>
        {
            var result = await service.GetAreasAsync(onlyActive);
            var response = new ApiResponse<IEnumerable<GetAreasResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Areas obtenidas correctamente"
            };
            return Results.Ok(response);
        })
        .WithName("GetAreas")
        .Produces<ApiResponse<IEnumerable<GetAreasResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
