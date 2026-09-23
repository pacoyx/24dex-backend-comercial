public record InspectionSummarySneakerResponse(
    long Id,
    int? BrandId,
    string? BrandName,
    string? PrimaryColor,
    string? SecondaryColor,
    bool HasLaces,
    bool HasInsoles,
    string? Observations
);

public record InspectionSummaryDetailResponse(
    int WorkGuideDetailId,
    int ProductId,
    string ProductName,
    ItemInspectionResponse? Inspection,
    InspectionSummarySneakerResponse? Sneaker
);

public record InspectionSummaryResponse(
    int WorkGuideId,
    string WorkGuideNumber,
    List<InspectionSummaryDetailResponse> Details
);
