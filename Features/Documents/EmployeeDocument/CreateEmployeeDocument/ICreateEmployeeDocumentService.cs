public interface ICreateEmployeeDocumentService
{
    Task<EmployeeDocumentResponseDto?> CreateEmployeeDocumentAsync(long employeeId, CreateEmployeeDocumentRequestDto request);
}
