public interface IGetAttendancesService
{
    Task<IEnumerable<AttendanceListItemDto>> GetAttendancesAsync(long? employeeId, DateOnly? startDate, DateOnly? endDate);
}
