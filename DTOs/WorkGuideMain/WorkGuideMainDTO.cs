using System.ComponentModel.DataAnnotations.Schema;

public class WorkGuideMainDTO
{
    public string SerieGuia { get; set; } = "";
    public string NumeroGuia { get; set; } = "";
    public DateTime FechaOperacion { get; set; }
    public DateTime FechaHoraEntrega { get; set; }
    public string MensajeAlertas { get; set; } = "";
    public string Observaciones { get; set; } = "";
    public string TipoPago { get; set; } = "";
    public string DescripcionPago { get; set; } = "";
    public string TipoRecepcion { get; set; } = "";
    public string DireccionContacto { get; set; } = "";
    public string TelefonoContacto { get; set; } = "";
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Acuenta { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Saldo { get; set; }
    public int CustomerId { get; set; }
    public required IEnumerable<WorkGuideDetailDTO> WorkGuideDetailsDTO { get; set; }
}