using Microsoft.EntityFrameworkCore;

public class RegisterIncidentService : IRegisterIncidentService
{
    private readonly RecepcionDbContext _context;

    public RegisterIncidentService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceIncidentResponseDto?> RegisterIncidentAsync(long employeeId, AttendanceIncidentType type, RegisterIncidentRequestDto request)
    {
        var employeeExists = await _context.Employees.AnyAsync(x => x.Id == employeeId);
        if (!employeeExists)
        {
            return null;
        }

        var incident = new AttendanceIncident
        {
            EmployeeId = employeeId,
            Date = request.Date,
            Type = type,
            Hours = request.Hours,
            Observation = request.Observation
        };

        _context.AttendanceIncidents.Add(incident);
        await _context.SaveChangesAsync();

        return new AttendanceIncidentResponseDto
        {
            Id = incident.Id,
            EmployeeId = incident.EmployeeId,
            Date = incident.Date,
            Type = incident.Type,
            Hours = incident.Hours,
            Observation = incident.Observation
        };
    }
}
