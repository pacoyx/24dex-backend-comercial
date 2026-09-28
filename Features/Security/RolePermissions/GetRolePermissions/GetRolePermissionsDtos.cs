public record RolePermissionsResponse(
    int RoleId,
    string RoleName,
    List<PermissionResponse> Permissions
);
