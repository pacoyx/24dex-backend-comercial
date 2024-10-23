using System.ComponentModel.DataAnnotations.Schema;

public class CashBoxMainCreateDTO
{    
    public DateTime FechaCaja { get; set; }
    public DateTime FechaHoraApertura { get; set; }        
    [Column(TypeName = "decimal(18,2)")]
    public decimal SaldoInicial { get; set; }   
    public string Observaciones { get; set; } = "";        
    public int BranchSalesId { get; set; } // sucursal
    public int WorkShiftId { get; set; } // horario
    public int UserId { get; set; }    
}