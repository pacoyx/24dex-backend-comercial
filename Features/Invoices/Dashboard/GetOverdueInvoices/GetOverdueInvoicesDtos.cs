public record OverdueInvoiceResponseDto(
    long InvoiceId,
    string DocumentNumber,
    string CustomerName,
    decimal Balance,
    int DaysPastDue
);
