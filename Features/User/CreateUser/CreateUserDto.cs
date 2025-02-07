public record CreateUserDto(string Name, string UserName, string Password, string Role, string Email);
public record CreateUserResponseDto(int id, string Name, string UserName, string Role);