public interface IRegisterIncidentService
{
    Task<AttendanceIncidentResponseDto?> RegisterIncidentAsync(long employeeId, AttendanceIncidentType type, RegisterIncidentRequestDto request);
}
