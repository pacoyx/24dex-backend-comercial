public record ExpenseDashboardSummaryResponse(
    decimal CurrentMonthExpenses,
    decimal CurrentMonthOperational,
    decimal CurrentMonthAdministrative,
    int ExpenseCount
);
