public record PurchaseDashboardSummaryResponseDto(
    decimal MonthPurchases,
    decimal AccountsPayable,
    int PurchaseCount,
    int ActiveSuppliers
);
