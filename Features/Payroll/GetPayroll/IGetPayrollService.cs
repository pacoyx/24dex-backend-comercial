public interface IGetPayrollService
{
    Task<IEnumerable<PayrollSummaryDto>?> GetPayrollAsync(long payrollPeriodId);
}
