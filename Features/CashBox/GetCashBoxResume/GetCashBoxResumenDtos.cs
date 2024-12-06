public record GetCashBoxResumeResponseDto(
    string Usuario,
    string TipoPago,
    decimal TotalAdelanto,
    decimal TotalImporte,
    IEnumerable<GetCashBoxResumeDetalleResponseDto> Detalle
);

public record GetCashBoxResumeDetalleResponseDto(
    decimal Adelanto,
    decimal Importe,
    string TipoPago,
    int? CustomerId,
    string Cliente,
    string serie,
    string numero
);