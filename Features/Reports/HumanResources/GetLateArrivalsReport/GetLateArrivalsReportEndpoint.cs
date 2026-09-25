public static class GetLateArrivalsReportEndpoint
{
    public static RouteGroupBuilder MapGetLateArrivalsReport(this RouteGroupBuilder group)
    {
        group.MapGet("/late-arrivals", async (IGetIncidentsService service, long? employeeId, DateOnly? startDate, DateOnly? endDate) =>
        {
            var result = await service.GetIncidentsAsync(employeeId, AttendanceIncidentType.LateArrival, startDate, endDate);
            return Results.Ok(new ApiResponse<IEnumerable<IncidentListItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Reporte de tardanzas obtenido correctamente"
            });
        })
        .WithName("GetLateArrivalsReport")
        .Produces<ApiResponse<IEnumerable<IncidentListItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
