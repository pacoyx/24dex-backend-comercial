public record GetProdServicesResponseDto(
    int TotalCount,
    List<GetProdServicesResponseDatosDto> Servicios
    );

public record GetProdServicesResponseDatosDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Status,
    int CatServiceId,
    string IsPeso,
    string IsLavado
);