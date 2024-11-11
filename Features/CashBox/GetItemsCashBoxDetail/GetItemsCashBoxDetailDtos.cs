public record GetCashBoxDetailResponseDto(
     int Id,
     string TipoComprobante,
     string SerieComprobante,
     string NumComprobante,
     DateTime FechaComprobante,
     decimal Importe,
     decimal Adelanto,
     string TipoPago,
     string DescripcionPago,
     string Observaciones,       
     Customer? Customer
);