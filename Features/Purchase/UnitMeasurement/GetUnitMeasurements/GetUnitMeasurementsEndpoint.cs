public static class GetUnitMeasurementsEndpoint
{
    public static RouteHandlerBuilder MapGetUnitMeasurements(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/", async (IGetUnitMeasurementsService getUnitMeasurementsService) =>
        {
            var responseDto = await getUnitMeasurementsService.GetUnitMeasurementsAsync();

            var response = new ApiResponse<IEnumerable<GetUnitMeasurementsResponseDto>>
            {
                Success = true,
                Data = responseDto,
                StatusCode = StatusCodes.Status200OK,
                Message = "Get Unit Measurements successfully"
            };

            return Results.Ok(response);
        })
        .WithName("GetUnitMeasurements")
        .WithTags("Get Unit Measurements")
        .Produces<ApiResponse<IEnumerable<GetUnitMeasurementsResponseDto>>>(StatusCodes.Status200OK)
        .Produces<ApiResponse<IEnumerable<GetUnitMeasurementsResponseDto>>>(StatusCodes.Status400BadRequest)
        .Produces<ApiResponse<IEnumerable<GetUnitMeasurementsResponseDto>>>(StatusCodes.Status500InternalServerError)
        // .RequireAuthorization()
        .CacheOutput();
    }

    public static RouteHandlerBuilder MapGetUnitMeasurementsShort(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/short", async (IGetUnitMeasurementsService getUnitMeasurementsService) =>
        {
            var responseDto = await getUnitMeasurementsService.GetUnitMeasurementsComboAsync();

            var response = new ApiResponse<IEnumerable<GetUnitMeasurementsComboResponseDto>>
            {
                Success = true,
                Data = responseDto,
                StatusCode = StatusCodes.Status200OK,
                Message = "Get Unit Measurements successfully"
            };

            return Results.Ok(response);
        })
        .WithName("GetUnitMeasurementsShort")
        .WithTags("Get Unit Measurements Short")
        .Produces<ApiResponse<IEnumerable<GetUnitMeasurementsResponseDto>>>(StatusCodes.Status200OK)
        .Produces<ApiResponse<IEnumerable<GetUnitMeasurementsResponseDto>>>(StatusCodes.Status400BadRequest)
        .Produces<ApiResponse<IEnumerable<GetUnitMeasurementsResponseDto>>>(StatusCodes.Status500InternalServerError)
        .RequireAuthorization()
        .CacheOutput();
    }
}