public static class UpdateUnitMeasurementEndpoint
{
    public static RouteGroupBuilder MapUpdateUnitMeasurement(this RouteGroupBuilder group)
    {
        group.MapPut("/", async (UpdateUnitMeasurementRequestDto request, IUpdateUnitMeasurementService service) =>
        {
            var result = await service.UpdateUnitMeasurementAsync(request);
            if (result != "OK")
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = result
                });
            }

            var response = new ApiResponse<string>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Unidad de medida actualizada correctamente."
            };

            return Results.Ok(response);
        })
        .WithName("UpdateUnitMeasurement")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Purchase");

        return group;
    }
}
