using Microsoft.EntityFrameworkCore;

public class RegisterLoanService : IRegisterLoanService
{
    private readonly RecepcionDbContext _context;

    public RegisterLoanService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeLoanResponseDto?> RegisterLoanAsync(long employeeId, LoanType loanType, RegisterLoanRequestDto request)
    {
        var employeeExists = await _context.Employees.AnyAsync(x => x.Id == employeeId);
        if (!employeeExists)
        {
            return null;
        }

        var loan = new EmployeeLoan
        {
            EmployeeId = employeeId,
            LoanType = loanType,
            LoanDate = request.LoanDate,
            Amount = request.Amount,
            Balance = request.Amount,
            Observation = request.Observation,
            IsClosed = false
        };

        _context.EmployeeLoans.Add(loan);
        await _context.SaveChangesAsync();

        return new EmployeeLoanResponseDto
        {
            Id = loan.Id,
            EmployeeId = loan.EmployeeId,
            LoanType = loan.LoanType,
            LoanDate = loan.LoanDate,
            Amount = loan.Amount,
            Balance = loan.Balance,
            Observation = loan.Observation,
            IsClosed = loan.IsClosed
        };
    }
}
