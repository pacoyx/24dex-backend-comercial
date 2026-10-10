using System.Security.Claims;

public static class CustomerPrincipalExtensions
{
    public static int? GetCustomerId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst("customerId")?.Value;
        return int.TryParse(value, out var id) ? id : null;
    }

    public static long? GetCustomerUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst("customerUserId")?.Value;
        return long.TryParse(value, out var id) ? id : null;
    }

    public static string? GetCustomerRole(this ClaimsPrincipal user)
    {
        return user.FindFirst("role")?.Value;
    }
}
