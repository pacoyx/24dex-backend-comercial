public record PickupGuideDetailRevisionRequest(long PickupGuideDetailId, decimal NewQuantity, string Reason);

public record CreatePickupGuideRevisionRequest(long EmployeeId, string? Observation, List<PickupGuideDetailRevisionRequest> Details);

public record PickupGuideDetailRevisionResponse(
    long Id,
    long PickupGuideDetailId,
    string ServiceItemName,
    decimal OriginalQuantity,
    decimal NewQuantity,
    string Reason
);

public record PickupGuideRevisionResponse(
    long Id,
    long PickupGuideId,
    long EmployeeId,
    string EmployeeName,
    string Observation,
    DateTime RevisionDate,
    List<PickupGuideDetailRevisionResponse> Details
);
