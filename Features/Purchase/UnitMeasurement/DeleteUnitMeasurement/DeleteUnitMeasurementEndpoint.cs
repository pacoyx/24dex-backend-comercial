public static class DeleteUnitMeasurementEndpoint
{
    public static RouteGroupBuilder MapDeleteUnitMeasurement(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (int id, IDeleteUnitMeasurementService service) =>
        {
            var result = await service.DeleteUnitMeasurementAsync(id);
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
                Message = "Unidad de medida desactivada correctamente."
            };

            return Results.Ok(response);
        })
        .WithName("DeleteUnitMeasurement")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Purchase");

        return group;
    }
}
