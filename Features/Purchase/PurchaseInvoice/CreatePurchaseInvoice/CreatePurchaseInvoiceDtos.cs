public record PurchaseInvoiceRequestDto
{
    public int SupplierId { get; set; }
    public string InvoiceType { get; set; } = string.Empty;
    public string InvoiceSerie { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceIssueDate { get; set; } = DateTime.Now;
    public DateTime InvoiceExpirationDate { get; set; } = DateTime.Now;
    public string TypePayment { get; set; } = string.Empty; // contado / credito    
    public int DaysCredit { get; set; } = 0; // Number of days for credit payment
    public string PaymentMethod { get; set; } = string.Empty; // e.g., "Cash", "Credit Card"
    public decimal Total { get; set; }
    public ICollection<PurchaseInvoiceDetailsDto>? PurchaseInvoiceDetails { get; set; }
}

public record PurchaseInvoiceResponseDto
{
    public int Id { get; set; }
}

public record PurchaseInvoiceDetailsDto
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public string ProductName { get; set; } = string.Empty;
}