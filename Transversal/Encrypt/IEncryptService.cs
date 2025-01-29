public interface IEncryptService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}