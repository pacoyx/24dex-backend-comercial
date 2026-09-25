public static class GetPayrollReportEndpoint
{
    public static RouteGroupBuilder MapGetPayrollReport(this RouteGroupBuilder group)
    {
        group.MapGet("/payroll", async (IGetPayrollReportService service, long? employeeId, long? payrollPeriodId, DateOnly? startDate, DateOnly? endDate) =>
        {
            var result = await service.GetPayrollReportAsync(employeeId, payrollPeriodId, startDate, endDate);
            return Results.Ok(new ApiResponse<IEnumerable<PayrollReportItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Reporte de planillas obtenido correctamente"
            });
        })
        .WithName("GetPayrollReport")
        .Produces<ApiResponse<IEnumerable<PayrollReportItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
