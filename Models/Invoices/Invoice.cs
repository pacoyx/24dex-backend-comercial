
public class Invoice
{
    public long Id { get; set; }

    public int CustomerId { get; set; }

    public int BranchId { get; set; }

    public int DocumentTypeId { get; set; }

    public string Serie { get; set; } = string.Empty;

    public string Numero { get; set; } = string.Empty;

    public string CurrencyId { get; set; } = "PEN";

    public decimal ExchangeRate { get; set; } = 1.0m;

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Igv { get; set; }

    public decimal Total { get; set; }

    public decimal AmountPaid { get; set; }

    public decimal Balance { get; set; }

    public InvoiceStatus Status { get; set; }

    public string? Notes { get; set; }

    public Customer Customer { get; set; } = null!;

    public ICollection<InvoiceDetail> Details { get; set; }
        = new List<InvoiceDetail>();

    public ICollection<InvoicePayment> Payments { get; set; }
        = new List<InvoicePayment>();
}