public class Expense
{
    public long Id { get; set; }

    public int BranchSalesId { get; set; }

    public int ExpenseCategoryId { get; set; }

    public ExpenseSourceType SourceType { get; set; }

    public DateTime ExpenseDate { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool HasSupportingDocument { get; set; }

    public string? DocumentNumber { get; set; }

    public string? SupplierName { get; set; }

    public string? Observation { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public BranchSales BranchSales { get; set; } = null!;

    public ExpenseCategory ExpenseCategory { get; set; } = null!;

    public ICollection<ExpenseAttachment> Attachments { get; set; }
        = new List<ExpenseAttachment>();
}
