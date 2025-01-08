public record GetCashBoxResumeResponseDto(
    int CajaId,
    string Usuario,
    string TipoPago,
    decimal TotalAdelanto,
    decimal TotalImporte
    // IEnumerable<GetCashBoxResumeDetalleResponseDto> Detalle
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

public record GetInfoCashMain(
    int cajaId,
    string Usuario,
    DateTime FechaHoraApertura,
    decimal SaldoInicial,
    decimal SaldoFinal,
    decimal TotalIngreso,
    decimal TotalSalida,
    string EstadoCaja,
    int userId
);