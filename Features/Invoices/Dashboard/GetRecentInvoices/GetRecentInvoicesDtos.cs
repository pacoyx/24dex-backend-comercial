public record RecentInvoiceResponseDto(
    long InvoiceId,
    DateTime IssueDate,
    string CustomerName,
    string DocumentNumber,
    decimal Total,
    string Status
);
