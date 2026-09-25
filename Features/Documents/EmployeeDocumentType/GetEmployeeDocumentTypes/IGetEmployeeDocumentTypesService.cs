public interface IGetEmployeeDocumentTypesService
{
    Task<IEnumerable<EmployeeDocumentTypeResponseDto>> GetEmployeeDocumentTypesAsync();
}
