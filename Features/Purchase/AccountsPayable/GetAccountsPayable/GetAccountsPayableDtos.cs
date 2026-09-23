public record AccountPayableResponse(
    int SupplierId,
    string SupplierName,
    int PurchaseInvoiceId,
    string DocumentNumber,
    decimal Total,
    decimal AmountPaid,
    decimal Balance,
    DateTime DueDate,
    PurchaseInvoiceStatus Status,
    int OverdueDays
);
