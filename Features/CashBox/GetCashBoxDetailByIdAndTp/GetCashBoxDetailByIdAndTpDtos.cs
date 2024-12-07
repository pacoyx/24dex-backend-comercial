public record GetCashBoxDetalleByIdyTpResponseDto(
    decimal Adelanto,
    decimal Importe,
    string TipoPago,
    int? CustomerId,
    string Cliente,
    string serie,
    string numero    
);