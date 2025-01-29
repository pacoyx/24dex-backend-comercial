using Microsoft.EntityFrameworkCore;

public class GetUsersService : IGetUsersService
{
     private readonly RecepcionDbContext _context;

    public GetUsersService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}