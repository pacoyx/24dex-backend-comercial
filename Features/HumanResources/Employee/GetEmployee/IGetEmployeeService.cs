public interface IGetEmployeeService
{
    Task<EmployeeDetailResponseDto?> GetEmployeeAsync(long id);
}
