public interface IDeleteUserService
{
    Task<User?> GetUser(int id);
    Task Delete(User user);
}