using System.ComponentModel.DataAnnotations.Schema;

public class CashBoxMain
{
    public int Id { get; set; }
    public DateTime FechaCaja { get; set; }
    public DateTime FechaHoraApertura { get; set; }    
    public DateTime? FechaHoraCierre { get; set; }
    public string EstadoCaja { get; set; } = "";
    [Column(TypeName = "decimal(18,2)")]
    public decimal SaldoInicial { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal SaldoFinal { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalIngreso { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSalida { get; set; }
    public string Observaciones { get; set; } = "";
    public string ObservacionesCierre { get; set; } = "";
    public string EstadoRegistro { get; set; } = "";        
    public int BranchSalesId { get; set; } // sucursal
    public int WorkShiftId { get; set; } // horario
    public int UserId { get; set; }
    public IEnumerable<CashBoxDetail>? CashBoxDetails { get; set; }
}