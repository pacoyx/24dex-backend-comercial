using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class WorkGuideMain
{
    public int Id { get; set; }
    public string SerieGuia { get; set; } = "";
    public string NumeroGuia { get; set; } = "";
    public DateTime FechaOperacion { get; set; }
    public DateTime FechaHoraEntrega { get; set; }
    public string MensajeAlertas { get; set; } = "";
    public string Observaciones { get; set; } = "";
    [MaxLength(2)]
    public string TipoPago { get; set; } = ""; // E: Efectivo, T: Tarjeta, D: Deposito, QR: Codigo QR, SP: Sin Pago
    public string DescripcionPago { get; set; } = "";
    public string TipoRecepcion { get; set; } = ""; // D: Delivery, R: Recepcion
    public string DireccionContacto { get; set; } = "";
    public string TelefonoContacto { get; set; } = "";
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Acuenta { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Saldo { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public required IEnumerable<WorkGuideDetail> WorkGuideDetails { get; set; }
    [MaxLength(2)]
    public string EstadoPago { get; set; } = "";    // PE: Pendiente, PA: Pagado, AN: Anulado
    public DateTime? FechaPago   { get; set; }
    [MaxLength(2)]
    public string EstadoRegistro { get; set; } = "";  // A: Activo, I: Inactivo    
    [MaxLength(2)]
    public string EstadoSituacion { get; set; } = "";  // P: Pendiente, E: Entregado; D: Devuelto
    public DateTime? FechaRecojo   { get; set; }
    public int AlertMsgId { get; set; }
    public AlertMsg? AlertMsg { get; set; }
    public string TipoPagoCancelacion { get; set; } = ""; // E: Efectivo, T: Tarjeta, D: Deposito, QR: Codigo QR, SP: Sin Pago

}