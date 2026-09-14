public record PrintJobDetailResponse(
    int ProductId,
    string ProductName,
    decimal Cant,
    decimal Precio,
    decimal Total,
    string Observaciones,
    string Ubicacion,
    string Identificador
);

public record PendingPrintQueueResponse(
    long Id,
    int WorkGuideId,
    int PrinterId,
    string DocumentType,
    DateTime CreatedAt,
    int RetryCount,
    PrintStatus Status
);

public record PrintJobResponse(
    long PrintQueueId,
    int WorkGuideId,
    string SerieGuia,
    string NumeroGuia,
    DateTime FechaOperacion,
    DateTime FechaHoraEntrega,
    string Cliente,
    string TelefonoContacto,
    string DireccionContacto,
    string TipoPago,
    string DescripcionPago,
    decimal Total,
    decimal Acuenta,
    decimal Saldo,
    string Observaciones,
    string MensajeAlertas,
    int PrinterId,
    List<PrintJobDetailResponse> Detalles
);
