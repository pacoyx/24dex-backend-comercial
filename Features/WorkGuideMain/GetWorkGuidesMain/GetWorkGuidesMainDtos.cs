public record ResponseWguidesmDto(
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
     int CustomerId
);