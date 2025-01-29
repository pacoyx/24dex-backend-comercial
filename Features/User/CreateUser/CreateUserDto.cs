public record CreateUserDto(string Name, string UserName, string Password, string Role, string Email);
public record CreateUserResponseDto(string Name, string UserName, string Role);