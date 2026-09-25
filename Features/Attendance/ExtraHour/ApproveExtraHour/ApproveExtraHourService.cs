using Microsoft.EntityFrameworkCore;

public class ApproveExtraHourService : IApproveExtraHourService
{
    private readonly RecepcionDbContext _context;

    public ApproveExtraHourService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<ExtraHourResponseDto?> ApproveExtraHourAsync(long id)
    {
        var extraHour = await _context.EmployeeExtraHours.FirstOrDefaultAsync(x => x.Id == id);
        if (extraHour == null)
        {
            return null;
        }

        extraHour.Approved = true;
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
