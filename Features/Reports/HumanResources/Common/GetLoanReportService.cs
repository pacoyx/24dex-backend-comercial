using Microsoft.EntityFrameworkCore;

public class GetLoanReportService : IGetLoanReportService
{
    private readonly RecepcionDbContext _context;

    public GetLoanReportService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoanReportItemDto>> GetLoanReportAsync(LoanType loanType, long? employeeId, DateTime? startDate, DateTime? endDate, bool? isClosed)
    {
        var query = _context.EmployeeLoans
            .AsNoTracking()
            .Include(x => x.Employee)
            .Where(x => x.LoanType == loanType)
            .AsQueryable();

        if (employeeId.HasValue)
        {
            query = query.Where(x => x.EmployeeId == employeeId.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(x => x.LoanDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x => x.LoanDate <= endDate.Value);
        }

        if (isClosed.HasValue)
        {
            query = query.Where(x => x.IsClosed == isClosed.Value);
        }

        return await query
            .OrderByDescending(x => x.LoanDate)
            .Select(x => new LoanReportItemDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                LoanDate = x.LoanDate,
                Amount = x.Amount,
                Balance = x.Balance,
                IsClosed = x.IsClosed,
                Observation = x.Observation
            })
            .ToListAsync();
    }
}
