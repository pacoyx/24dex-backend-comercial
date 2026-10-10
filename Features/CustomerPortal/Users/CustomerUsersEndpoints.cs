public static class CustomerUsersEndpoints
{
    internal static CustomerUserResponse ToResponse(CustomerUser user) => new(
        user.Id, user.FullName, user.Email, user.UserName,
        user.CustomerRoleId, user.CustomerRole.Name, user.IsActive, user.CreatedAt, user.LastLoginAt
    );
}
