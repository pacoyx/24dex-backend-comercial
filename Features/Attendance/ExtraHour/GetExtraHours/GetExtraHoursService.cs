using Microsoft.EntityFrameworkCore;

public class GetExtraHoursService : IGetExtraHoursService
{
    private readonly RecepcionDbContext _context;

    public GetExtraHoursService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExtraHourListItemDto>> GetExtraHoursAsync(long? employeeId, bool? approved, DateOnly? startDate, DateOnly? endDate)
    {
        var query = _context.EmployeeExtraHours
            .AsNoTracking()
            .Include(x => x.Employee)
            .AsQueryable();

        if (employeeId.HasValue)
        {
            query = query.Where(x => x.EmployeeId == employeeId.Value);
        }

        if (approved.HasValue)
        {
            query = query.Where(x => x.Approved == approved.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(x => x.Date >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x => x.Date <= endDate.Value);
        }

        return await query
            .OrderByDescending(x => x.Date)
            .Select(x => new ExtraHourListItemDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                Date = x.Date,
                Hours = x.Hours,
                Observation = x.Observation,
                Approved = x.Approved
            })
            .ToListAsync();
    }
}
