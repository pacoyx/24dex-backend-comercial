using Microsoft.EntityFrameworkCore;

public class GetPayrollService : IGetPayrollService
{
    private readonly RecepcionDbContext _context;

    public GetPayrollService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PayrollSummaryDto>?> GetPayrollAsync(long payrollPeriodId)
    {
        var periodExists = await _context.PayrollPeriods.AnyAsync(x => x.Id == payrollPeriodId);
        if (!periodExists)
        {
            return null;
        }

        var payrolls = await _context.Payrolls
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.PayrollDetails)
            .Where(x => x.PayrollPeriodId == payrollPeriodId)
            .OrderBy(x => x.Employee!.LastName).ThenBy(x => x.Employee!.FirstName)
            .ToListAsync();

        return payrolls.Select(x => new PayrollSummaryDto
        {
            Id = x.Id,
            EmployeeId = x.EmployeeId,
            EmployeeName = $"{x.Employee!.FirstName} {x.Employee.LastName}",
            GrossAmount = x.GrossAmount,
            Discounts = x.Discounts,
            NetAmount = x.NetAmount,
            GeneratedAt = x.GeneratedAt,
            Details = x.PayrollDetails!.Select(d => new PayrollDetailLineDto
            {
                ConceptType = d.ConceptType,
                Description = d.Description,
                Amount = d.Amount
            }).ToList()
        });
    }
}
