public record CustomerContactRequestDto(
    string FullName,
    string? Position,
    string PhoneNumber,
    string? Email,
    bool IsPrimary,
    bool ReceivePickupGuideEmails,
    bool ReceiveInvoices,
    bool ReceivePortalNotifications
);

public record CustomerContactResponseDto(
    long Id,
    int CustomerId,
    string FullName,
    string? Position,
    string PhoneNumber,
    string? Email,
    bool IsPrimary,
    bool ReceivePickupGuideEmails,
    bool ReceiveInvoices,
    bool ReceivePortalNotifications
);
