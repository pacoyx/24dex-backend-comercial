public class CancelPurchaseInvoiceResult
{
    public bool NotFound { get; set; }
    public string? Error { get; set; }
    public PurchaseInvoiceStatus? PaymentStatus { get; set; }
}
