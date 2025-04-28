using System.Collections.ObjectModel;

public record GetPurchaseInvoiceResponseDto
{
    public int Id { get; set; }
    public string InvoiceType { get; set; } = string.Empty;
    public string InvoiceSerie { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceIssueDate { get; set; } = DateTime.Now;
    public DateTime InvoiceExpirationDate { get; set; } = DateTime.Now;
    public string TypePayment { get; set; } = string.Empty;
    public int DaysCredit { get; set; } = 0;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; } = DateTime.Now;
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
}

public record GetPurchaseInvoiceByIdResponseDto
{
    public int Id { get; set; }
    public string InvoiceType { get; set; } = string.Empty;
    public string InvoiceSerie { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceIssueDate { get; set; } = DateTime.Now;
    public DateTime InvoiceExpirationDate { get; set; } = DateTime.Now;
    public string TypePayment { get; set; } = string.Empty;
    public int DaysCredit { get; set; } = 0;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; } = DateTime.Now;
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierRuc { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Igv { get; set; }    
    public string CurrencyId { get; set; } = "";
    public decimal ExchangeRate { get; set; }
    public string Comments { get; set; } = "";
    public ICollection<GetPurchaseInvoiceByIdDetailsResponseDto> PurchaseInvoiceDetails { get; set; } = new Collection<GetPurchaseInvoiceByIdDetailsResponseDto>();
}


public record GetPurchaseInvoiceByIdDetailsResponseDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public string Comments { get; set; } = string.Empty;
}