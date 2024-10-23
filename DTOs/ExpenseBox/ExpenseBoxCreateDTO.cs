using System.ComponentModel.DataAnnotations.Schema;

public class ExpenseBoxCreateDTO
{
    public string CategoryGasto { get; set; } = "";
    public string PersonalAutoriza { get; set; } = "";
    public DateTime FechaGasto { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Importe { get; set; }
    public string DetallesEgreso { get; set; } = "";
    public string EstadoRegistro { get; set; } = "";
}