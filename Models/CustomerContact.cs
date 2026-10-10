using System.ComponentModel.DataAnnotations;

public class CustomerContact
{
    public long Id { get; set; }

    public int CustomerId { get; set; }

    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Position { get; set; }

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Email { get; set; }

    public bool IsPrimary { get; set; }

    public bool ReceivePickupGuideEmails { get; set; }

    public bool ReceiveInvoices { get; set; }

    public bool ReceivePortalNotifications { get; set; }

    public Customer Customer { get; set; } = null!;
}
