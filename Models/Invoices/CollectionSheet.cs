
public class CollectionSheet
{
    public long Id { get; set; }

    public int BranchId { get; set; }

    public string Number { get; set; } = string.Empty;

    public DateTime OpenDate { get; set; }

    public DateTime? CloseDate { get; set; }

    public decimal TotalAmount { get; set; }

    public CollectionSheetStatus Status { get; set; }

    public string? Remarks { get; set; }

    public ICollection<InvoicePayment> Payments { get; set; }
        = new List<InvoicePayment>();
}