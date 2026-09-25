public interface IRegisterAttendanceService
{
    Task<AttendanceResponseDto> RegisterAttendanceAsync(RegisterAttendanceRequestDto request);
}
