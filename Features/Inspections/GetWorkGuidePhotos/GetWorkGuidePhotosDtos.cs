public record WorkGuidePhotoResponse(
    long Id,
    string FileName,
    string? OriginalFileName,
    string ContentType,
    long FileSize,
    DateTime CreatedAt
);

public record WorkGuidePhotosResponse(
    int WorkGuideId,
    List<WorkGuidePhotoResponse> Photos
);
