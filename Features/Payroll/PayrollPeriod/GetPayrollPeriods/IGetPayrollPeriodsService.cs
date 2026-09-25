public interface IGetPayrollPeriodsService
{
    Task<IEnumerable<PayrollPeriodResponseDto>> GetPayrollPeriodsAsync();
}
