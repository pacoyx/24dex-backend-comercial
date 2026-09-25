using Microsoft.EntityFrameworkCore;

public class GetEmployeeDocumentTypesService : IGetEmployeeDocumentTypesService
{
    private readonly RecepcionDbContext _context;

    public GetEmployeeDocumentTypesService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeDocumentTypeResponseDto>> GetEmployeeDocumentTypesAsync()
    {
        return await _context.EmployeeDocumentTypes
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new EmployeeDocumentTypeResponseDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }
}
