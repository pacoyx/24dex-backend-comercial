public static class GetExtraHoursEndpoint
{
    public static RouteGroupBuilder MapGetExtraHours(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetExtraHoursService service, long? employeeId, bool? approved, DateOnly? startDate, DateOnly? endDate) =>
        {
            var result = await service.GetExtraHoursAsync(employeeId, approved, startDate, endDate);
            return Results.Ok(new ApiResponse<IEnumerable<ExtraHourListItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Horas extras obtenidas correctamente"
            });
        })
        .WithName("GetExtraHours")
        .Produces<ApiResponse<IEnumerable<ExtraHourListItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
