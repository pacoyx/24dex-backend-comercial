public record GetCompanyByUserResponseDto(
    int Id,
    string NameComercial,
    string Description,
    string Address,
    string Email,
    string Phone,
    string WebSite,
    int UsuarioId,
    string NameCompany,
    string DocumentType,
    string NumberType,
    string Logo,
    string Facebook,
    string Twitter,
    string Instagram,
    string Status
);