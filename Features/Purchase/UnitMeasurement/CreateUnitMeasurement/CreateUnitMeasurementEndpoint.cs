public static class CreateUnitMeasurementEndpoint
{
    public static RouteHandlerBuilder MapCreateUnitMeasurement(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/", async (ICreateUnitMeasurementService createUnitMeasurementService, CreateUnitMeasurementRequestDto request) =>
        {            
            var responseDto = await createUnitMeasurementService.CreateUnitMeasurementAsync(request);

            var response = new ApiResponse<CreateUnitMeasurementResponseDto>
            {
                Success = true,
                Data = responseDto,
                StatusCode = StatusCodes.Status200OK,
                Message = "Create Unit Measurement successfully"
            };

            return Results.Ok(response);
        })
        .WithName("CreateUnitMeasurement")
        .WithTags("Create Unit Measurement")
        .Produces<ApiResponse<CreateUnitMeasurementResponseDto>>(StatusCodes.Status200OK)
        .Produces<ApiResponse<CreateUnitMeasurementResponseDto>>(StatusCodes.Status400BadRequest)
        .Produces<ApiResponse<CreateUnitMeasurementResponseDto>>(StatusCodes.Status500InternalServerError)
        .RequireAuthorization()
        .CacheOutput("JWT_Aware_Cache");
    }
}