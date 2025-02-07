public class GetCompanyService : IGetCompanyService
{
 
    private readonly RecepcionDbContext _context;

    public GetCompanyService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        return company;
    }
}