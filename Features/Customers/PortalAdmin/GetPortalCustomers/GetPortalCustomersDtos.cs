public record PortalCustomerAdminItem(
    int CustomerId,
    string CustomerName,
    bool PortalEnabled,
    int TotalUsers,
    int ActiveUsers,
    DateTime? LastLogin,
    string Status
);
