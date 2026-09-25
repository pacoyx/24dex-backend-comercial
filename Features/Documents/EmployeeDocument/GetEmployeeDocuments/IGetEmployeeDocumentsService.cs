public interface IGetEmployeeDocumentsService
{
    Task<IEnumerable<EmployeeDocumentResponseDto>> GetEmployeeDocumentsAsync(long employeeId);
}
