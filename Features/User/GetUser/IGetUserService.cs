public interface IGetUserService
{
    Task<User?> GetUser(int id);
}