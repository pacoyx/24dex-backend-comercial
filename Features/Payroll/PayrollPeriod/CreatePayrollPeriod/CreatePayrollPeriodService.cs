public class CreatePayrollPeriodService : ICreatePayrollPeriodService
{
    private readonly RecepcionDbContext _context;

    public CreatePayrollPeriodService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<PayrollPeriodResponseDto> CreatePayrollPeriodAsync(CreatePayrollPeriodRequestDto request)
    {
        var period = new PayrollPeriod
        {
            Name = request.Name,
            Frequency = request.Frequency,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsClosed = false
        };

        _context.PayrollPeriods.Add(period);
        await _context.SaveChangesAsync();

        return new PayrollPeriodResponseDto
        {
            Id = period.Id,
            Name = period.Name,
            Frequency = period.Frequency,
            StartDate = period.StartDate,
            EndDate = period.EndDate,
            IsClosed = period.IsClosed
        };
    }
}
