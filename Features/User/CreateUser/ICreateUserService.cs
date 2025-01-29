public interface ICreateUserService
{
    Task<User> Create(CreateUserDto userDto);
    Task<bool> ExistUser(string userName);
}