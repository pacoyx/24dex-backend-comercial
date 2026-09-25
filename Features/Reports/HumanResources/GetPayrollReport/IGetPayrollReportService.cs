public interface IGetPayrollReportService
{
    Task<IEnumerable<PayrollReportItemDto>> GetPayrollReportAsync(long? employeeId, long? payrollPeriodId, DateOnly? startDate, DateOnly? endDate);
}
