public record PickupGuideDetailRequest(int ServiceItemId, decimal Quantity, decimal? Weight, string? Observation);

public record PickupGuideDetailResponse(
    long Id,
    int ServiceItemId,
    string ServiceItemName,
    decimal Quantity,
    decimal? Weight,
    decimal UnitPrice,
    decimal Total,
    string? Observation
);

public record PickupGuideResponse(
    long Id,
    string GuideNumber,
    int CustomerId,
    string CustomerName,
    int BranchSalesId,
    DateTime PickupDate,
    PickupGuideStatus Status,
    string? Observation,
    long CustomerPriceListId,
    decimal TotalAmount,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    long? EmployeeId,
    string? EmployeeName,
    int? VehicleId,
    string? VehiclePlate,
    PickupGuideEmailStatus EmailDeliveryStatus,
    DateTime? EmailSentAt,
    List<PickupGuideDetailResponse> Details
);

public record PickupGuideSummaryResponse(
    long Id,
    string GuideNumber,
    int CustomerId,
    string CustomerName,
    int BranchSalesId,
    DateTime PickupDate,
    PickupGuideStatus Status,
    decimal TotalAmount,
    DateTime CreatedAt
);
