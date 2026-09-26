public record CustomerAccountSummaryResponseDto(
    int CustomerId,
    string BusinessName,
    decimal CreditLimit,
    decimal CurrentBalance,
    decimal AvailableCredit,
    int PendingInvoices
);
