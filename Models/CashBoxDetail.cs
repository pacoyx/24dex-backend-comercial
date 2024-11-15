using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class CashBoxDetail
{
    public int Id { get; set; }    
    public string TipoComprobante { get; set; } = "";
    public string SerieComprobante { get; set; } = "";
    public string NumComprobante { get; set; } = "";
    public DateTime FechaComprobante { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Importe { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Adelanto { get; set; }
    public string TipoPago { get; set; } = ""; // E: Efectivo, T: Tarjeta, D: Deposito, QR: Codigo QR, SP: Sin Pago
    public string DescripcionPago { get; set; } = ""; // yape, plin, visa,etc
    public string Observaciones { get; set; } = "";
    public string EstadoRegistro { get; set; } = ""; // A: Activo, I: Inactivo
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int CashBoxMainId { get; set; }
    [JsonIgnore]
    public CashBoxMain? CashBoxMain { get; set; }

}
