public record RecentPurchaseResponseDto(
    int Id,
    string SupplierName,
    string DocumentNumber,
    DateTime InvoiceIssueDate,
    decimal Total,
    PurchaseInvoiceStatus PaymentStatus
);
