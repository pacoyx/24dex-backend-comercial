public record CreatePickupGuideRequest(
    int CustomerId,
    int BranchSalesId,
    DateTime PickupDate,
    string? Observation,
    List<PickupGuideDetailRequest> Details
);
