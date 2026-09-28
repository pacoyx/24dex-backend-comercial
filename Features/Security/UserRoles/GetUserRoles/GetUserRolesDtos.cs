public record UserRolesResponse(
    int UserId,
    string UserName,
    List<RoleResponse> Roles
);
