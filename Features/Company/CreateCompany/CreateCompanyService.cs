public class CreateCompanyService : ICreateCompanyService
{
    private readonly RecepcionDbContext _context;

    public CreateCompanyService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<Company> CreateCompany(CreateCompanyDto companyDto)
    {

        var company = new Company
        {
            NameComercial = companyDto.NameComercial,
            Description = companyDto.Description,
            NameCompany = companyDto.NameCompany,
            DocumentType = companyDto.DocumentType,
            NumberType = companyDto.NumberType,
            Address = companyDto.Address,
            Email = companyDto.Email,
            Phone = companyDto.Phone,
            WebSite = companyDto.Website,
            Status = "A",
            UsuarioId = companyDto.usuarioId,
            Logo = companyDto.Logo,
            Facebook = companyDto.Facebook,
            Twitter = companyDto.Twitter,
            Instagram = companyDto.Instagram,
        };

        await _context.Companies.AddAsync(company);
        _context.SaveChanges();
        return company;
    }

    public Task<bool> ExistUser(int usuarioId)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == usuarioId);
        if (user == null)
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }
}