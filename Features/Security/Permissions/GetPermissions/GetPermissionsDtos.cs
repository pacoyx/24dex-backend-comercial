public record PermissionResponse(
    int Id,
    string Code,
    string Description,
    bool IsActive,
    DateTime CreatedAt
);
