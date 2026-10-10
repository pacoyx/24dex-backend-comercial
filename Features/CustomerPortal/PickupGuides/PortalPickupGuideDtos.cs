public record PortalPickupGuideDetailResponse(
    long Id,
    int ServiceItemId,
    string ServiceItemName,
    decimal Quantity,
    decimal? Weight,
    decimal UnitPrice,
    decimal Total,
    string? Observation
);

public record PortalPickupGuideSummaryResponse(
    long Id,
    string GuideNumber,
    int BranchSalesId,
    DateTime PickupDate,
    PickupGuideStatus Status,
    decimal TotalAmount,
    DateTime CreatedAt
);

public record PortalPickupGuideResponse(
    long Id,
    string GuideNumber,
    int BranchSalesId,
    DateTime PickupDate,
    PickupGuideStatus Status,
    string? Observation,
    decimal TotalAmount,
    DateTime CreatedAt,
    List<PortalPickupGuideDetailResponse> Details
);

public record PortalPickupGuideAttachmentResponse(
    long Id,
    PickupGuideAttachmentType AttachmentType,
    string OriginalFileName,
    DateTime CreatedAt
);
