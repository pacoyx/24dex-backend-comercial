public interface IGetEmployeesService
{
    Task<GetEmployeesResponseDto> GetEmployeesAsync(int pageNumber, int pageSize, bool? onlyActive, int? areaId, string? search);
}
