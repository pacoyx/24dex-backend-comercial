public static class GetEmployeesEndpoint
{
    public static RouteGroupBuilder MapGetEmployees(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetEmployeesService service, int? pageNumber, int? pageSize, bool? onlyActive, int? areaId, string? search) =>
        {
            var page = pageNumber is null or <= 0 ? 1 : pageNumber.Value;
            var size = pageSize is null or <= 0 ? 20 : pageSize.Value;

            var result = await service.GetEmployeesAsync(page, size, onlyActive, areaId, search);
            var response = new ApiResponse<GetEmployeesResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Trabajadores obtenidos correctamente"
            };
            return Results.Ok(response);
        })
        .WithName("GetEmployees")
        .Produces<ApiResponse<GetEmployeesResponseDto>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
