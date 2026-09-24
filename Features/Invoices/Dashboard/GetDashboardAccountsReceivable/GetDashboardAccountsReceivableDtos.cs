public record DashboardAccountReceivableResponseDto(
    long InvoiceId,
    string CustomerName,
    string DocumentNumber,
    decimal Total,
    decimal AmountPaid,
    decimal Balance,
    string Status,
    int DaysPastDue
);
