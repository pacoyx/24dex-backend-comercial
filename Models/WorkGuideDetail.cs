using DexterCompany.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class WorkGuideDetail
{
    public int Id { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cant { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    public string Observaciones { get; set; } = "";
    public string TipoLavado { get; set; } = ""; // A:Agua; S:Seco
    public string Ubicacion { get; set; } = ""; // L:lavanderia; P:Planta; A:Almacen
    public string EstadoTrabajo { get; set; } = ""; // P: Pendiente, E: En Proceso, F: Finalizado
    public int ProductId { get; set; }
    public ProdService? Product { get; set; }
    public int WorkGuideMainId { get; set; }
    public WorkGuideMain? WorkGuideMain { get; set; }
    public string EstadoRegistro { get; set; } = "";  // A: Activo, I: Inactivo    
    public int CompanyId { get; set; }
    public string EstadoSituacion { get; set; } = "";  // P: Pendiente, E: Entregado; D: Devuelto
    public string EstadoPago { get; set; } = "";    // PE: Pendiente, PA: Pagado, AN: Anulado
    public DateTime? FechaRecojo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
}
