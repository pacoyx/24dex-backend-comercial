

public record RequestCashBoxDetailCreateDto
(
    string TipoComprobante,
    string SerieComprobante,
    string NumComprobante,
    DateTime FechaComprobante,
    decimal Importe,
    decimal Adelanto,
    string TipoPago,
    string DescripcionPago,
    string Observaciones,
    string EstadoRegistro,
    int CustomerId,
    int CashBoxMainId
);


public record RequestCashBoxDetailCreateOtherInDto
(
    string SerieComprobante,
    string NumComprobante,
    decimal Importe,
    string TipoPago,
    string DescripcionPago,
    string Observaciones,
    int userId,
    int? CustomerId
);

public record RequestSplitPayCash
(
    int CashBoxDetailId,
    List<SplitPayCashDetail> SplitPayCashDetail
);

public record SplitPayCashDetail
(    
    decimal Importe,
    string TipoPago 
);