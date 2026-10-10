public record UpdatePickupGuideRequest(
    int BranchSalesId,
    DateTime PickupDate,
    string? Observation,
    long? EmployeeId,
    int? VehicleId,
    List<PickupGuideDetailRequest> Details
);
