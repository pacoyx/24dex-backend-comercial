public static class CreatePositionEndpoint
{
    public static RouteGroupBuilder MapCreatePosition(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (ICreatePositionService service, CreatePositionRequestDto request) =>
        {
            var result = await service.CreatePositionAsync(request);
            var response = new ApiResponse<CreatePositionResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Cargo creado correctamente"
            };
            return Results.Ok(response);
        })
        .WithName("CreatePosition")
        .Produces<ApiResponse<CreatePositionResponseDto>>(StatusCodes.Status201Created)
        .RequireAuthorization();

        return group;
    }
}
