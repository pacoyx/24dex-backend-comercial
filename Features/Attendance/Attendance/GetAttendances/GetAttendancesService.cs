using Microsoft.EntityFrameworkCore;

public class GetAttendancesService : IGetAttendancesService
{
    private readonly RecepcionDbContext _context;

    public GetAttendancesService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AttendanceListItemDto>> GetAttendancesAsync(long? employeeId, DateOnly? startDate, DateOnly? endDate)
    {
        var query = _context.Attendances
            .AsNoTracking()
            .Include(x => x.Employee)
            .AsQueryable();

        if (employeeId.HasValue)
        {
            query = query.Where(x => x.EmployeeId == employeeId.Value);
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
            .Select(x => new AttendanceListItemDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee!.FirstName + " " + x.Employee.LastName,
                Date = x.Date,
                CheckIn = x.CheckIn,
                CheckOut = x.CheckOut
            })
            .ToListAsync();
    }
}
