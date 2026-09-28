public record TopExpenseResponse(
    long Id,
    DateTime ExpenseDate,
    string BranchName,
    string CategoryName,
    decimal Amount,
    string Description
);
