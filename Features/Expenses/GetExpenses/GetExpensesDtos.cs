public record ExpenseSummaryResponse(
    long Id,
    DateTime ExpenseDate,
    int BranchSalesId,
    string BranchName,
    int ExpenseCategoryId,
    string CategoryName,
    ExpenseSourceType SourceType,
    decimal Amount,
    string Description,
    bool HasSupportingDocument,
    string? DocumentNumber,
    string? SupplierName
);
