public static class CreateAreaEndpoint
{
    public static RouteGroupBuilder MapCreateArea(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (ICreateAreaService service, CreateAreaRequestDto request) =>
        {
            var result = await service.CreateAreaAsync(request);
            var response = new ApiResponse<CreateAreaResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Area creada correctamente"
            };
            return Results.Ok(response);
        })
        .WithName("CreateArea")
        .Produces<ApiResponse<CreateAreaResponseDto>>(StatusCodes.Status201Created)
        .RequireAuthorization();

        return group;
    }
}
