public record CreateExpenseRequest(
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

public record ExpenseResponse(
    long Id,
    int BranchSalesId,
    string BranchName,
    int ExpenseCategoryId,
    string CategoryName,
    ExpenseSourceType SourceType,
    DateTime ExpenseDate,
    decimal Amount,
    string Description,
    bool HasSupportingDocument,
    string? DocumentNumber,
    string? SupplierName,
    string? Observation,
    bool IsActive,
    DateTime CreatedAt
);
