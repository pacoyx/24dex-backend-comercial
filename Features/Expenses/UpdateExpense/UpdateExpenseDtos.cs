public record UpdateExpenseRequest(
    int BranchSalesId,
    int ExpenseCategoryId,
    ExpenseSourceType SourceType,
    DateTime ExpenseDate,
    decimal Amount,
    string Description,
    bool HasSupportingDocument,
    string? DocumentNumber,
    string? SupplierName,
    string? Observation
);
