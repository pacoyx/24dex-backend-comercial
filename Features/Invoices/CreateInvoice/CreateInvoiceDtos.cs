public record CreateInvoiceRequest(
    int CustomerId,
    int BranchId,
    int DocumentTypeId,
    string Serie,
    string Numero,
    string CurrencyId,
    decimal ExchangeRate,
    DateTime IssueDate,
    DateTime DueDate,
    string? Notes,
    List<CreateInvoiceDetailRequest> Details
);

public record CreateInvoiceDetailRequest(
    int ProductId,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal Discount
);

public record CreateInvoiceResponse(
    long Id,
    string Serie,
    string Numero,
    string CurrencyId,
    decimal ExchangeRate,
    decimal SubTotal,
    decimal Igv,
    decimal Total,
    InvoiceStatus Status
);
