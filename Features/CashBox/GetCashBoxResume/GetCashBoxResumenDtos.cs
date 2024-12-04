public record GetCashBoxResumeResponseDto(
    string Usuario,
    string TipoPago,
    decimal TotalAdelanto,
    decimal TotalImporte
);