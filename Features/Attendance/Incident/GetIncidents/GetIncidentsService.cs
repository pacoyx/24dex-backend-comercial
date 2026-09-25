using Microsoft.EntityFrameworkCore;

public class GetIncidentsService : IGetIncidentsService
{
    private readonly RecepcionDbContext _context;

    public GetIncidentsService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IncidentListItemDto>> GetIncidentsAsync(long? employeeId, AttendanceIncidentType? type, DateOnly? startDate, DateOnly? endDate)
    {
        var query = _context.AttendanceIncidents
            .AsNoTracking()
            .Include(x => x.Employee)
            .AsQueryable();

        if (employeeId.HasValue)
        {
            query = query.Where(x => x.EmployeeId == employeeId.Value);
        }

        if (type.HasValue)
        {
            query = query.Where(x => x.Type == type.Value);
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
            .Select(x => new IncidentListItemDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                Date = x.Date,
                Type = x.Type,
                Hours = x.Hours,
                Observation = x.Observation
            })
            .ToListAsync();
    }
}
