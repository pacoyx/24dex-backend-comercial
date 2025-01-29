public class DeleteUserService : IDeleteUserService
{
    private readonly RecepcionDbContext _context;

    public DeleteUserService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);        
        return user;
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}