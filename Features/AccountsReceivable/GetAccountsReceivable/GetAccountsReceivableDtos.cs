public record AccountReceivableResponse(
    int CustomerId,
    string CustomerName,
    long InvoiceId,
    string Serie,
    string Numero,
    decimal Total,
    decimal AmountPaid,
    decimal Balance,
    InvoiceStatus Status,
    int OverdueDays
);
