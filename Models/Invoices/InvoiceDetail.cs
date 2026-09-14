
public class InvoiceDetail
{
    public long Id { get; set; }

    public long InvoiceId { get; set; }

    public int ProductId { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal Total { get; set; }

    public Invoice Invoice { get; set; } = null!;
}
