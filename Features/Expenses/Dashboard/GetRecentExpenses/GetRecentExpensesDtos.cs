public record RecentExpenseResponse(
    long Id,
    DateTime ExpenseDate,
    string BranchName,
    string CategoryName,
    decimal Amount,
    string Description
);
