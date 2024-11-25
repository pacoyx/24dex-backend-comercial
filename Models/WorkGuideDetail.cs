using DexterCompany.Models;
using System.ComponentModel.DataAnnotations;
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
    [MaxLength(1)]
    public string TipoLavado { get; set; } = ""; // A:Agua; S:Seco
    [MaxLength(1)]
    public string Ubicacion { get; set; } = ""; // L:lavanderia; P:Planta; A:Almacen
    [MaxLength(1)]
    public string EstadoTrabajo { get; set; } = ""; // P: Pendiente, E: En Proceso, F: Finalizado
    public int ProductId { get; set; }
    public ProdService? Product { get; set; }
    public int WorkGuideMainId { get; set; }
    public WorkGuideMain? WorkGuideMain { get; set; }
    [MaxLength(1)]
    public string EstadoRegistro { get; set; } = "";  // A: Activo, I: Inactivo    
    public int CompanyId { get; set; }
    [MaxLength(1)]
    public string EstadoSituacion { get; set; } = "";  // P: Pendiente, E: Entregado; D: Devuelto
    [MaxLength(2)]
    public string EstadoPago { get; set; } = "";    // PE: Pendiente, PA: Pagado, AN: Anulado
    public DateTime? FechaRecojo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    [MaxLength(1)]
    public string Identificador { get; set; } = "";
}
