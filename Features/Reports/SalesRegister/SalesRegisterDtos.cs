public sealed class SalesRegisterRowDto
{
    public long InvoiceId { get; set; }

    public DateTime IssueDate { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string SerieNumero { get; set; } = string.Empty;

    public decimal SubTotal { get; set; }

    public decimal Igv { get; set; }

    public decimal Total { get; set; }

    public string CurrencyId { get; set; } = string.Empty;

    public decimal ExchangeRate { get; set; }

    public decimal AmountPaid { get; set; }

    public decimal Balance { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string TypePayment { get; set; } = string.Empty;

    public int DaysCredit { get; set; }

    public string Status { get; set; } = string.Empty;
}

public sealed class SalesRegisterFilter
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int? BranchId { get; set; }

    public int? CustomerId { get; set; }

    public InvoiceStatus? Status { get; set; }
}

public sealed class SalesRegisterTotalsDto
{
    public int CantidadDocumentos { get; set; }

    public decimal TotalVentas { get; set; }

    public decimal TotalCobrado { get; set; }

    public decimal TotalPendiente { get; set; }
}

public sealed class SalesRegisterReportResponse
{
    public List<SalesRegisterRowDto> Rows { get; set; } = new();

    public SalesRegisterTotalsDto Totals { get; set; } = new();
}
