public record InvoiceSummaryResponse(
    long Id,
    int CustomerId,
    string CustomerName,
    string Serie,
    string Numero,
    string CurrencyId,
    decimal ExchangeRate,
    DateTime IssueDate,
    DateTime DueDate,
    decimal Total,
    decimal AmountPaid,
    decimal Balance,
    string PaymentMethod,
    string TypePayment,
    int DaysCredit,
    InvoiceStatus Status
);
