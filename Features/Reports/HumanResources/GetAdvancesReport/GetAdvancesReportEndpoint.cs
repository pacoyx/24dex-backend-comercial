public static class GetAdvancesReportEndpoint
{
    public static RouteGroupBuilder MapGetAdvancesReport(this RouteGroupBuilder group)
    {
        group.MapGet("/advances", async (IGetLoanReportService service, long? employeeId, DateTime? startDate, DateTime? endDate, bool? isClosed) =>
        {
            var result = await service.GetLoanReportAsync(LoanType.SalaryAdvance, employeeId, startDate, endDate, isClosed);
            return Results.Ok(new ApiResponse<IEnumerable<LoanReportItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Reporte de adelantos obtenido correctamente"
            });
        })
        .WithName("GetAdvancesReport")
        .Produces<ApiResponse<IEnumerable<LoanReportItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
