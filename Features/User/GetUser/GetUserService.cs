public class GetUserService : IGetUserService
{
    private readonly RecepcionDbContext _context;

    public GetUserService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}