public interface IDeleteEmployeeDocumentService
{
    Task<bool> DeleteEmployeeDocumentAsync(long employeeId, long documentId);
}
