using Microsoft.EntityFrameworkCore;

public class GetCompanyByUserService : IGetCompanyByUserService
{
    private readonly RecepcionDbContext _context;

    public GetCompanyByUserService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetCompanyByUser(int userId)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.UsuarioId == userId);
        return company;
    }
}