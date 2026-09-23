public record WorkGuidePhotoResponse(
    long Id,
    int WorkGuideDetailId,
    long ItemInspectionId,
    string FileName,
    string BlobUrl,
    DateTime CreatedAt
);

public record WorkGuidePhotosResponse(
    int WorkGuideId,
    List<WorkGuidePhotoResponse> Photos
);
