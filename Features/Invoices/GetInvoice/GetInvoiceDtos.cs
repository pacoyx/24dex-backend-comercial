public record InvoiceDetailItemResponse(
    long Id,
    int ProductId,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal Total
);

public record InvoiceResponse(
    long Id,
    int CustomerId,
    string CustomerName,
    int BranchId,
    int DocumentTypeId,
    string Serie,
    string Numero,
    DateTime IssueDate,
    DateTime DueDate,
    decimal SubTotal,
    decimal Igv,
    decimal Total,
    decimal AmountPaid,
    decimal Balance,
    InvoiceStatus Status,
    string? Notes,
    List<InvoiceDetailItemResponse> Details,
    List<InvoicePaymentResponse> Payments
);
