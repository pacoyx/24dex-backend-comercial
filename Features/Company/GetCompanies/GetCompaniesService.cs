using Microsoft.EntityFrameworkCore;

public class GetCompaniesService : IGetCompaniesService
{
      private readonly RecepcionDbContext _context;

    public GetCompaniesService(RecepcionDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<GetCompaniesDto>> GetCompanies()
    {
        var companies = await _context.Companies.ToListAsync();
        return companies.Select(c => new GetCompaniesDto(
            c.Id,
            c.NameComercial,
            c.Description ?? string.Empty,
            c.Address ?? string.Empty,
            c.Email ?? string.Empty,
            c.Phone ?? string.Empty,
            c.WebSite ?? string.Empty,
            c.UsuarioId,
            c.NameCompany,
            c.DocumentType,
            c.NumberType,
            c.Logo ?? string.Empty,
            c.Facebook ?? string.Empty,
            c.Twitter ?? string.Empty,
            c.Instagram ?? string.Empty
        ));
    
    }
}