public record CustomerContactRequestDto(
    string FullName,
    string? Position,
    string PhoneNumber,
    string? Email,
    bool IsPrimary
);

public record CustomerContactResponseDto(
    long Id,
    int CustomerId,
    string FullName,
    string? Position,
    string PhoneNumber,
    string? Email,
    bool IsPrimary
);
