public class UpdateCompanyService : IUpdateCompanyService
{
    private readonly RecepcionDbContext _context;

    public UpdateCompanyService(RecepcionDbContext context)
    {
        _context = context;
    }
   
    public async Task<Company?> UpdateCompany(UpdateCompanyDto companyDto)
    {
        var company = await _context.Companies.FindAsync(companyDto.Id);
        if (company == null)
        {
            return null;
        }

        company.NameComercial = companyDto.NameComercial;
        company.Description = companyDto.Description;
        company.NameCompany = companyDto.NameCompany;
        company.DocumentType = companyDto.DocumentType;
        company.NumberType = companyDto.NumberType;
        company.Address = companyDto.Address;
        company.Email = companyDto.Email;
        company.Phone = companyDto.Phone;
        company.WebSite = companyDto.Website;
        company.Status = companyDto.Status;
        company.UsuarioId = companyDto.usuarioId;
        company.Logo = companyDto.Logo;
        company.Facebook = companyDto.Facebook;
        company.Twitter = companyDto.Twitter;
        company.Instagram = companyDto.Instagram;

        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company?> GetCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        return company;
    }
}