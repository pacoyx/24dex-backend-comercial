using Microsoft.EntityFrameworkCore;

public class RegisterAttendanceService : IRegisterAttendanceService
{
    private readonly RecepcionDbContext _context;

    public RegisterAttendanceService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<AttendanceResponseDto> RegisterAttendanceAsync(RegisterAttendanceRequestDto request)
    {
        var attendance = await _context.Attendances
            .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.Date == request.Date);

        if (attendance == null)
        {
            attendance = new Attendance
            {
                EmployeeId = request.EmployeeId,
                Date = request.Date
            };
            _context.Attendances.Add(attendance);
        }

        if (request.CheckIn.HasValue)
        {
            attendance.CheckIn = request.CheckIn;
        }

        if (request.CheckOut.HasValue)
        {
            attendance.CheckOut = request.CheckOut;
        }

        await _context.SaveChangesAsync();

        return new AttendanceResponseDto
        {
            Id = attendance.Id,
            EmployeeId = attendance.EmployeeId,
            Date = attendance.Date,
            CheckIn = attendance.CheckIn,
            CheckOut = attendance.CheckOut
        };
    }
}
