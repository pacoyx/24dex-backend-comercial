public static class GetIncidentsEndpoint
{
    public static RouteGroupBuilder MapGetIncidents(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetIncidentsService service, long? employeeId, AttendanceIncidentType? type, DateOnly? startDate, DateOnly? endDate) =>
        {
            var result = await service.GetIncidentsAsync(employeeId, type, startDate, endDate);
            return Results.Ok(new ApiResponse<IEnumerable<IncidentListItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Incidencias obtenidas correctamente"
            });
        })
        .WithName("GetIncidents")
        .Produces<ApiResponse<IEnumerable<IncidentListItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
