public record CreatePickupGuideRequest(
    int CustomerId,
    int BranchSalesId,
    DateTime PickupDate,
    string? Observation,
    long? EmployeeId,
    int? VehicleId,
    List<PickupGuideDetailRequest> Details
);
