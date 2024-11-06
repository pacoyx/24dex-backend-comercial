public record UpdateWorkGuideInfoPayRequestDto(
    int Id,
    string TipoPago,
    string DescripcionPago,
    DateTime FechaPago,
    string EstadoPago,
    int idUser
);