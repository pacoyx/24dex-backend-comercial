public record CurrentUserResponse(
    int UserId,
    string Name,
    List<string> Roles,
    List<string> Permissions
);
