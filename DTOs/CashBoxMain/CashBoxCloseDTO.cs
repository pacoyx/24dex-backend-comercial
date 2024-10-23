
using System.ComponentModel.DataAnnotations.Schema;

public record CashBoxCloseDTO
{
    public int Id { get; set; }    
    public string EstadoCaja { get; set; } = "";
    [Column(TypeName = "decimal(18,2)")]
    public decimal SaldoFinal { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalIngreso { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSalida { get; set; }
    public string ObservacionesCierre { get; set; } = "";
}
