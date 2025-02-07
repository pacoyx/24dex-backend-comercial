public class DeleteCompanyService : IDeleteCompanyService
{
   private readonly RecepcionDbContext _context;

    public DeleteCompanyService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task DeleteCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            throw new Exception("Company not found");
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
    }

    public async Task<Company?> GetCompany(int id)
    {
        var company =  await _context.Companies.FindAsync(id);
        return company;
    }
}