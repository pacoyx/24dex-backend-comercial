public record CustomerUserResponse(
    long Id,
    string FullName,
    string Email,
    string UserName,
    long CustomerRoleId,
    string RoleName,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? LastLoginAt
);
