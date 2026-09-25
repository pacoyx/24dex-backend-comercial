using Microsoft.EntityFrameworkCore;

public class GetLoansService : IGetLoansService
{
    private readonly RecepcionDbContext _context;

    public GetLoansService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoanListItemDto>> GetLoansAsync(long? employeeId, LoanType? loanType, bool? isClosed)
    {
        var query = _context.EmployeeLoans
            .AsNoTracking()
            .Include(x => x.Employee)
            .AsQueryable();

        if (employeeId.HasValue)
        {
            query = query.Where(x => x.EmployeeId == employeeId.Value);
        }

        if (loanType.HasValue)
        {
            query = query.Where(x => x.LoanType == loanType.Value);
        }

        if (isClosed.HasValue)
        {
            query = query.Where(x => x.IsClosed == isClosed.Value);
        }

        return await query
            .OrderByDescending(x => x.LoanDate)
            .Select(x => new LoanListItemDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                LoanType = x.LoanType,
                LoanDate = x.LoanDate,
                Amount = x.Amount,
                Balance = x.Balance,
                Observation = x.Observation,
                IsClosed = x.IsClosed
            })
            .ToListAsync();
    }
}
