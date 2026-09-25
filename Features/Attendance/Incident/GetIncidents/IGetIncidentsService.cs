public interface IGetIncidentsService
{
    Task<IEnumerable<IncidentListItemDto>> GetIncidentsAsync(long? employeeId, AttendanceIncidentType? type, DateOnly? startDate, DateOnly? endDate);
}
