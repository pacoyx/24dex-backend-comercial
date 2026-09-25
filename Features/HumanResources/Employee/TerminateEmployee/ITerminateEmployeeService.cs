public interface ITerminateEmployeeService
{
    Task<EmployeeResponseDto?> TerminateEmployeeAsync(long id, TerminateEmployeeRequestDto request);
}
