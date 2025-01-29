using System.Security.Cryptography;
using System.Text;

public class EncryptService : IEncryptService
{
    public string HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hash = Convert.ToBase64String(passwordHash);
        var key = Convert.ToBase64String(hmac.Key);
        return $"{key}:{hash}";
    }

    public bool VerifyPassword(string password, string hash)
    {
        using var hmac = new HMACSHA512(Convert.FromBase64String(hash));
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(Convert.FromBase64String(hash));
    }
}