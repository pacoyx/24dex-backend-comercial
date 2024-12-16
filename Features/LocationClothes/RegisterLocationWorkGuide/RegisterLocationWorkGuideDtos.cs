public record CreateLocationWorkGuideRequestDto
(
    int LocationClothesId,
    IEnumerable<string> NumeroGuia,    
    string Comments
);