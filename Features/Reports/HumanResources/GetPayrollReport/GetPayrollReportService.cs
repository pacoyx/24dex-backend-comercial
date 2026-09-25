using Microsoft.EntityFrameworkCore;

public class GetPayrollReportService : IGetPayrollReportService
{
    private readonly RecepcionDbContext _context;

    public GetPayrollReportService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PayrollReportItemDto>> GetPayrollReportAsync(long? employeeId, long? payrollPeriodId, DateOnly? startDate, DateOnly? endDate)
    {
        var query = _context.Payrolls
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.PayrollPeriod)
            .AsQueryable();

        if (employeeId.HasValue)
        {
            query = query.Where(x => x.EmployeeId == employeeId.Value);
        }

        if (payrollPeriodId.HasValue)
        {
            query = query.Where(x => x.PayrollPeriodId == payrollPeriodId.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(x => x.PayrollPeriod!.StartDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x => x.PayrollPeriod!.EndDate <= endDate.Value);
        }

        return await query
            .OrderByDescending(x => x.GeneratedAt)
            .Select(x => new PayrollReportItemDto
            {
                PayrollId = x.Id,
                PayrollPeriodId = x.PayrollPeriodId,
                PayrollPeriodName = x.PayrollPeriod!.Name,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                GrossAmount = x.GrossAmount,
                Discounts = x.Discounts,
                NetAmount = x.NetAmount,
                GeneratedAt = x.GeneratedAt
            })
            .ToListAsync();
    }
}
