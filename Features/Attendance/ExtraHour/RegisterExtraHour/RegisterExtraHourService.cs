using Microsoft.EntityFrameworkCore;

public class RegisterExtraHourService : IRegisterExtraHourService
{
    private readonly RecepcionDbContext _context;

    public RegisterExtraHourService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<ExtraHourResponseDto?> RegisterExtraHourAsync(long employeeId, RegisterExtraHourRequestDto request)
    {
        var employeeExists = await _context.Employees.AnyAsync(x => x.Id == employeeId);
        if (!employeeExists)
        {
            return null;
        }

        var extraHour = new EmployeeExtraHour
        {
            EmployeeId = employeeId,
            Date = request.Date,
            Hours = request.Hours,
            Observation = request.Observation,
            Approved = false
        };

        _context.EmployeeExtraHours.Add(extraHour);
        await _context.SaveChangesAsync();

        return new ExtraHourResponseDto
        {
            Id = extraHour.Id,
            EmployeeId = extraHour.EmployeeId,
            Date = extraHour.Date,
            Hours = extraHour.Hours,
            Observation = extraHour.Observation,
            Approved = extraHour.Approved
        };
    }
}
