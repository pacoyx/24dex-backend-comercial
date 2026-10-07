public record UpdatePickupGuideRequest(
    int BranchSalesId,
    DateTime PickupDate,
    string? Observation,
    List<PickupGuideDetailRequest> Details
);
