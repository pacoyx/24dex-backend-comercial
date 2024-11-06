
public record WgmCreateDto
(
     string SerieGuia,
     string NumeroGuia,     
     string MensajeAlertas,
     string Observaciones,
     string TipoPago,
     string DescripcionPago,
     string TipoRecepcion,
     string DireccionContacto,
     string TelefonoContacto,
     decimal Total,
     decimal Acuenta,
     decimal Saldo,
     int CustomerId,
     IEnumerable<WgdCreateDto> WorkGuideDetailsDTO,
     int BranchStoreId,
     string TypeDocument,
     int UserId,
     string EstadoPago,     
     string EstadoRegistro,
     string EstadoSituacion     
);

public record WgdCreateDto(
     decimal Cant,
     decimal Precio,
     decimal Total,
     string Observaciones,
     string TipoLavado,
     string Ubicacion,
     string EstadoTrabajo,
     int ProductId,
     string EstadoRegistro,
     string EstadoSituacion,
     string EstadoPago
);