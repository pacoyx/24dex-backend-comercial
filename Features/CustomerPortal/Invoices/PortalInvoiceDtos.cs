public record PortalInvoiceDetailResponse(
    long Id,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal Total
);

public record PortalInvoiceSummaryResponse(
    long Id,
    string Serie,
    string Numero,
    string CurrencyId,
    DateTime IssueDate,
    DateTime DueDate,
    decimal Total,
    decimal AmountPaid,
    decimal Balance,
    InvoiceStatus Status
);

public record PortalInvoiceResponse(
    long Id,
    string Serie,
    string Numero,
    string CurrencyId,
    DateTime IssueDate,
    DateTime DueDate,
    decimal SubTotal,
    decimal Igv,
    decimal Total,
    decimal AmountPaid,
    decimal Balance,
    InvoiceStatus Status,
    string? Notes,
    List<PortalInvoiceDetailResponse> Details
);
