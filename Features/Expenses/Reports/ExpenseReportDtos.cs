public sealed class ExpenseReportFilter
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int? BranchSalesId { get; set; }

    public int? ExpenseCategoryId { get; set; }

    public ExpenseSourceType? SourceType { get; set; }
}

public sealed class ExpenseDetailRowDto
{
    public long Id { get; set; }

    public DateTime ExpenseDate { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public string SourceType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool HasSupportingDocument { get; set; }

    public string? DocumentNumber { get; set; }

    public string? SupplierName { get; set; }
}

public sealed class ExpenseAggregateRowDto
{
    public string Label { get; set; } = string.Empty;

    public int Count { get; set; }

    public decimal Amount { get; set; }
}

public sealed class ExpenseReportTotalsDto
{
    public int Count { get; set; }

    public decimal TotalAmount { get; set; }
}

public sealed class ExpenseDetailReportResponse
{
    public List<ExpenseDetailRowDto> Rows { get; set; } = new();

    public ExpenseReportTotalsDto Totals { get; set; } = new();
}

public sealed class ExpenseAggregateReportResponse
{
    public List<ExpenseAggregateRowDto> Rows { get; set; } = new();

    public ExpenseReportTotalsDto Totals { get; set; } = new();
}
