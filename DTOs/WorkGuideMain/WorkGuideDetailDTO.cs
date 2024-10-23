using DexterCompany.Models;

public class WorkGuideDetailDTO
{
    public decimal Cant { get; set; }
    public decimal Precio { get; set; }
    public decimal Total { get; set; }
    public string Observaciones { get; set; } = "";
    public string TipoLavado { get; set; } = "";
    public string Ubicacion { get; set; } = "";
    public string EstadoTrabajo { get; set; } = "";
    public int ProductId { get; set; }
    public ProdService? Product { get; set; }
    public string EstadoRegistro { get; set; } = "";
    public string EstadoSituacion { get; set; } = "";
    public string EstadoPago { get; set; } = "";
}
