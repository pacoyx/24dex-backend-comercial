public static class GetExtraHoursReportEndpoint
{
    public static RouteGroupBuilder MapGetExtraHoursReport(this RouteGroupBuilder group)
    {
        group.MapGet("/extra-hours", async (IGetExtraHoursService service, long? employeeId, bool? approved, DateOnly? startDate, DateOnly? endDate) =>
        {
            var result = await service.GetExtraHoursAsync(employeeId, approved, startDate, endDate);
            return Results.Ok(new ApiResponse<IEnumerable<ExtraHourListItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Reporte de horas extras obtenido correctamente"
            });
        })
        .WithName("GetExtraHoursReport")
        .Produces<ApiResponse<IEnumerable<ExtraHourListItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
