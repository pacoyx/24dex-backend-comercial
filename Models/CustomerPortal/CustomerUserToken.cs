public class CustomerUserToken
{
    public long Id { get; set; }

    public long CustomerUserId { get; set; }

    public string Token { get; set; } = string.Empty;

    public string Purpose { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }
}
