using System.Security.Cryptography;
using System.Text;

public class EncryptService : IEncryptService
{
    public string HashPassword(string password)
    {
        // Generate a salt
        byte[] salt;
        salt = new byte[16];
        RandomNumberGenerator.Fill(salt);

        // Derive a 256-bit subkey (use HMACSHA256 with 10000 iterations)
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(32);
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }
    }
    // Example usage
    public void ExampleUsage()
    {
        var encryptService = new EncryptService();
        var password = "mySecurePassword";
        var hashedPassword = encryptService.HashPassword(password);

        // Validate the password
        var isValid = encryptService.VerifyPassword(password, hashedPassword);
        Console.WriteLine($"Password is valid: {isValid}");
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            byte[] computedHash = pbkdf2.GetBytes(32);
            return computedHash.SequenceEqual(hash);
        }
    }

    // public bool VerifyPassword(string password, string hash)
    // {
    //     using var hmac = new HMACSHA512(Convert.FromBase64String(hash));
    //     var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    //     return computedHash.SequenceEqual(Convert.FromBase64String(hash));
    // }
}