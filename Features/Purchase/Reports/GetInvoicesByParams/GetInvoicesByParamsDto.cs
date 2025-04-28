public class InvoiceListPaginatorResponseDto
{
    public List<InvoiceDto> Invoices { get; set; } = new List<InvoiceDto>();
    public int TotalCount { get; set; }
}

public class InvoiceDto
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierRuc { get; set; } = string.Empty;
    public string InvoiceType { get; set; } = string.Empty;
    public string InvoiceSerie { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceIssueDate { get; set; } = DateTime.Now;
    public DateTime InvoiceExpirationDate { get; set; } = DateTime.Now;
    public string TypePayment { get; set; } = string.Empty;
    public int DaysCredit { get; set; } = 0;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Igv { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = "A";
    public string CurrencyId { get; set; } = "";
    public decimal ExchangeRate { get; set; }
    public string Comments { get; set; } = "";
    public ICollection<InvoiceDetailsDto>? Details { get; set; }
}


public class InvoiceDetailsDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public string Comments { get; set; } = string.Empty;
}