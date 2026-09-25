public sealed class PurchaseRegisterRowDto
{
    public long InvoiceId { get; set; }

    public DateTime IssueDate { get; set; }

    public string SupplierName { get; set; } = string.Empty;

    public string SupplierRuc { get; set; } = string.Empty;

    public string SerieNumero { get; set; } = string.Empty;

    public decimal SubTotal { get; set; }

    public decimal Igv { get; set; }

    public decimal Total { get; set; }

    public string CurrencyId { get; set; } = string.Empty;

    public decimal ExchangeRate { get; set; }

    public decimal AmountPaid { get; set; }

    public decimal Balance { get; set; }

    public string Status { get; set; } = string.Empty;
}

public sealed class PurchaseRegisterFilter
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int? SupplierId { get; set; }

    public PurchaseInvoiceStatus? Status { get; set; }
}

public sealed class PurchaseRegisterTotalsDto
{
    public int CantidadDocumentos { get; set; }

    public decimal TotalCompras { get; set; }

    public decimal TotalPagado { get; set; }

    public decimal TotalPendiente { get; set; }
}

public sealed class PurchaseRegisterReportResponse
{
    public List<PurchaseRegisterRowDto> Rows { get; set; } = new();

    public PurchaseRegisterTotalsDto Totals { get; set; } = new();
}
