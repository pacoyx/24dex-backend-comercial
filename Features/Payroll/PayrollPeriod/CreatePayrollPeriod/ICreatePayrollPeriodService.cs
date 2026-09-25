public interface ICreatePayrollPeriodService
{
    Task<PayrollPeriodResponseDto> CreatePayrollPeriodAsync(CreatePayrollPeriodRequestDto request);
}
