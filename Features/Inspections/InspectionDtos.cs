public record ItemInspectionResponse(
    long Id,
    int WorkGuideDetailId,
    bool HasStains,
    bool HasTears,
    bool HasFading,
    bool HasMissingParts,
    string? Notes,
    DateTime CreatedAt,
    int PhotoCount
);

public record ItemPhotoResponse(
    long Id,
    long ItemInspectionId,
    string FileName,
    string? OriginalFileName,
    string BlobUrl,
    DateTime CreatedAt
);
