public record GetCashBoxResumeResponseDto(
    string Usuario,
    string TipoPago,
    decimal TotalAdelanto,
    decimal TotalImporte,
    IEnumerable<GetCashBoxResumeDetalleResponseDto> Detalle
);

public record GetCashBoxResumeDetalleResponseDto(
    decimal AdelantoUsuario,
    decimal Importe,
    string TipoPago,
    int? CustomerId,
    Customer? Cliente
);