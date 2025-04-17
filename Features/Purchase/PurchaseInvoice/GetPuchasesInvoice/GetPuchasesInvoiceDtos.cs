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
    public ICollection<PurchaseInvoiceDetailsDto> PurchaseInvoiceDetails { get; set; } = new Collection<PurchaseInvoiceDetailsDto>();
}