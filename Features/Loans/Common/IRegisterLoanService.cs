public interface IRegisterLoanService
{
    Task<EmployeeLoanResponseDto?> RegisterLoanAsync(long employeeId, LoanType loanType, RegisterLoanRequestDto request);
}
