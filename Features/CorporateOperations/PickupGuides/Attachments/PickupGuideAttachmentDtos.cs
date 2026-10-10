public record PickupGuideAttachmentResponse(
    long Id,
    long PickupGuideId,
    PickupGuideAttachmentType AttachmentType,
    string OriginalFileName,
    string ContentType,
    long FileSize,
    bool IsCurrent,
    DateTime CreatedAt
);
