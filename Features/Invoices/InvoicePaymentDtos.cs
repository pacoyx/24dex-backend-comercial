public record InvoicePaymentResponse(
    long Id,
    long InvoiceId,
    int PaymentMethodId,
    string PaymentMethodName,
    DateTime PaymentDate,
    decimal Amount,
    string? OperationNumber,
    string? Observation,
    long? CollectionSheetId
);
