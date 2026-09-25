public static class GetLoansReportEndpoint
{
    public static RouteGroupBuilder MapGetLoansReport(this RouteGroupBuilder group)
    {
        group.MapGet("/loans", async (IGetLoanReportService service, long? employeeId, DateTime? startDate, DateTime? endDate, bool? isClosed) =>
        {
            var result = await service.GetLoanReportAsync(LoanType.Loan, employeeId, startDate, endDate, isClosed);
            return Results.Ok(new ApiResponse<IEnumerable<LoanReportItemDto>>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Reporte de préstamos obtenido correctamente"
            });
        })
        .WithName("GetLoansReport")
        .Produces<ApiResponse<IEnumerable<LoanReportItemDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return group;
    }
}
