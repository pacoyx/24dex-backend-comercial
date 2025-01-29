public interface IUpdateUserService
{
    Task<User?> UpdateUser(int id, UpdateUserRequestDto updateUserRequestDto);
}