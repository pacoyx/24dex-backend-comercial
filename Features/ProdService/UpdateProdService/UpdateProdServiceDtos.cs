public record UpdateProdServiceDto(
int Id,
string Name,
string Description,
decimal Price,
string Status,
int CatServiceId,
string IsPeso,
string IsLavado
);