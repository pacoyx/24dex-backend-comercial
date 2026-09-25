using Microsoft.EntityFrameworkCore;

public class CreateEmployeeDocumentService : ICreateEmployeeDocumentService
{
    private readonly RecepcionDbContext _context;

    public CreateEmployeeDocumentService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDocumentResponseDto?> CreateEmployeeDocumentAsync(long employeeId, CreateEmployeeDocumentRequestDto request)
    {
        var employeeExists = await _context.Employees.AnyAsync(x => x.Id == employeeId);
        if (!employeeExists)
        {
            return null;
        }

        var document = new EmployeeDocument
        {
            EmployeeId = employeeId,
            DocumentTypeId = request.DocumentTypeId,
            FileName = request.FileName,
            BlobUrl = request.BlobUrl,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.EmployeeDocuments.Add(document);
        await _context.SaveChangesAsync();

        var documentType = await _context.EmployeeDocumentTypes.AsNoTracking().FirstAsync(x => x.Id == document.DocumentTypeId);

        return new EmployeeDocumentResponseDto
        {
            Id = document.Id,
            EmployeeId = document.EmployeeId,
            DocumentTypeId = document.DocumentTypeId,
            DocumentTypeName = documentType.Name,
            FileName = document.FileName,
            BlobUrl = document.BlobUrl,
            Notes = document.Notes,
            CreatedAt = document.CreatedAt
        };
    }
}
