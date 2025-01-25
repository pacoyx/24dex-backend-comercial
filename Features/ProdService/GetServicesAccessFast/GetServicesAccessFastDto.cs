


using Microsoft.Identity.Client;

public record class GetServicesAccessFastResponseDto(
    int Id,
    int ProdServiceID,
    string ShortName,
    string IconName,
    string Status,
    decimal Price
);