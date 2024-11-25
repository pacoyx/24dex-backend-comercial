

public record GetWgsByCustomersPaginatorResponseDto(
    int TotalCount,
    IEnumerable<WgByCustomersResponseDto> Guias
    );

public record WgByCustomersResponseDto(
    int Id,
    string SerieGuia,
    string NumeroGuia,
    DateTime FechaOperacion,
    DateTime FechaHoraEntrega,
    string Observaciones,
    string TipoPago,
    string DescripcionPago,
    string TipoRecepcion,
    decimal Total,
    decimal Acuenta,
    decimal Saldo,
    string NombreCliente,
    string TelefonoCliente,
    string EstadoPago,
    DateTime? FechaPag,
    string EstadoRegistro,
    string EstadoSituacion,
    DateTime? FechaRecojo,
    string TipoPagoCancelacion,
    WgByCustomersDetailResponseDto[] Detalles
);



public class WgByCustomersDetailResponseDto
{
    public int Id { get; set; }
    public decimal Cant { get; set; }
    public decimal Precio { get; set; }
    public decimal Total { get; set; }
    public string Servicio { get; set; } = "";
    public string Observaciones { get; set; } = "";    
    public string Ubicacion { get; set; } = "";
    public string EstadoTrabajo { get; set; } = "";            
    public string EstadoRegistro { get; set; } = "";
    public string EstadoSituacion { get; set; } = "";
    public string EstadoPago { get; set; } = "";
    public DateTime? FechaRecojo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
}