public interface IUpdateEmployeeService
{
    Task<EmployeeResponseDto?> UpdateEmployeeAsync(long id, UpdateEmployeeRequestDto request);
}
