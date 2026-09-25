public static class GetPayrollPeriodsEndpoint
{
    public static RouteGroupBuilder MapGetPayrollPeriods(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetPayrollPeriodsService service) =>
        {
            var result = await service.GetPayrollPeriodsAsync();
            return Results.Ok(new ApiResponse<IEnumerable<PayrollPeriodResponseDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Periodos de planilla obtenidos correctamente"
            });
        })
        .WithName("GetPayrollPeriods")
        .Produces<ApiResponse<IEnumerable<PayrollPeriodResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
