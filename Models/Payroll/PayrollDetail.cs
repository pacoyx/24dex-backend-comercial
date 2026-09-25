using System.ComponentModel.DataAnnotations;

public class PayrollDetail
{
    public long Id { get; set; }

    public long PayrollId { get; set; }

    public PayrollConceptType ConceptType { get; set; }

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public Payroll? Payroll { get; set; }
}
