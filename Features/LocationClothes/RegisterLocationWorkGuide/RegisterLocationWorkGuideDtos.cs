public record CreateLocationWorkGuideRequestDto
(
    int LocationClothesId,
    IEnumerable<GuiaDto> Guias,
    string Comments
);

public record GuiaDto
(
    string NumeroGuia,
    string Referencia,
    bool IsSystem
);