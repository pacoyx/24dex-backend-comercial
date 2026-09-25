using Microsoft.EntityFrameworkCore;

public class DeleteEmployeeDocumentService : IDeleteEmployeeDocumentService
{
    private readonly RecepcionDbContext _context;

    public DeleteEmployeeDocumentService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteEmployeeDocumentAsync(long employeeId, long documentId)
    {
        var document = await _context.EmployeeDocuments
            .FirstOrDefaultAsync(x => x.Id == documentId && x.EmployeeId == employeeId);

        if (document == null)
        {
            return false;
        }

        _context.EmployeeDocuments.Remove(document);
        await _context.SaveChangesAsync();

        return true;
    }
}
