public interface IGetUsersService
{
    Task<List<User>> GetUsers();
}