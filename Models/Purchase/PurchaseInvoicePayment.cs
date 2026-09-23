using System.ComponentModel.DataAnnotations.Schema;

public class PurchaseInvoicePayment
{
    public long Id { get; set; }

    public int PurchaseInvoiceId { get; set; }

    public int PaymentMethodId { get; set; }

    public DateTime PaymentDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public string? OperationNumber { get; set; }

    public string? Observation { get; set; }

    public PurchaseInvoice PurchaseInvoice { get; set; } = null!;

    public PaymentMethod PaymentMethod { get; set; } = null!;
}
