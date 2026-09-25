public interface IGetLoanReportService
{
    Task<IEnumerable<LoanReportItemDto>> GetLoanReportAsync(LoanType loanType, long? employeeId, DateTime? startDate, DateTime? endDate, bool? isClosed);
}
