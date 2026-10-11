using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class CurrentUserExtensions
{
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        return int.TryParse(value, out var id) ? id : null;
    }

    public static long? GetEmployeeId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst("employeeId")?.Value;
        return long.TryParse(value, out var id) ? id : null;
    }
}
