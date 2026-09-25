using Microsoft.EntityFrameworkCore;

public class GetPayrollPeriodsService : IGetPayrollPeriodsService
{
    private readonly RecepcionDbContext _context;

    public GetPayrollPeriodsService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PayrollPeriodResponseDto>> GetPayrollPeriodsAsync()
    {
        return await _context.PayrollPeriods
            .AsNoTracking()
            .OrderByDescending(x => x.StartDate)
            .Select(x => new PayrollPeriodResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Frequency = x.Frequency,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsClosed = x.IsClosed
            })
            .ToListAsync();
    }
}
