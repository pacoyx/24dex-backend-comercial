public interface ICreateEmployeeService
{
    Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeRequestDto request);
}
