public interface IGeneratePayrollService
{
    Task<GeneratePayrollResponseDto?> GeneratePayrollAsync(long payrollPeriodId);
}
