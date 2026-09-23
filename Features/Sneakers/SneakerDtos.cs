public record SneakerDetailResponse(
    long Id,
    int WorkGuideDetailId,
    int? BrandId,
    string? BrandName,
    string? PrimaryColor,
    string? SecondaryColor,
    bool HasLaces,
    bool HasInsoles,
    string? Observations,
    DateTime CreatedAt
);
