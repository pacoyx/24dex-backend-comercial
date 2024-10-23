using DexterCompany.Models;

public record ResponseWgmDto
(
     string SerieGuia,
     string NumeroGuia,
     DateTime FechaOperacion,
     DateTime FechaHoraEntrega,
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
     IEnumerable<ResponseWgdDto> WorkGuideDetailsDTO
);


public record ResponseWgdDto(
    decimal Cant,
    decimal Precio,
    decimal Total,
    string Observaciones,
    string TipoLavado,
    string Ubicacion,
    string EstadoTrabajo,
    int ProductId,
    ProdService Product,
    string EstadoRegistro,
    string EstadoSituacion,
    string EstadoPago
);