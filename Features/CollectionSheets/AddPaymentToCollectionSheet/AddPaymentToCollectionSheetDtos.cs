public record AddPaymentToCollectionSheetRequest(
    long InvoiceId,
    int PaymentMethodId,
    decimal Amount,
    string? OperationNumber,
    string? Observation
);
