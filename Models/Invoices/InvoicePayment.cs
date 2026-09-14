
public class InvoicePayment
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public int PaymentMethodId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string? OperationNumber { get; set; }

    public string? Observation { get; set; }

    public long? CollectionSheetId { get; set; }

    public Invoice Invoice { get; set; } = null!;

    public PaymentMethod PaymentMethod { get; set; } = null!;

    public CollectionSheet? CollectionSheet { get; set; }
}