using System.ComponentModel.DataAnnotations;

public class EmployeeExtraHour
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public DateOnly Date { get; set; }

    public decimal Hours { get; set; }

    [MaxLength(500)]
    public string? Observation { get; set; }

    public bool Approved { get; set; }

    public Employee? Employee { get; set; }
}
