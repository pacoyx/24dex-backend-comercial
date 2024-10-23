public record UpdateNumbersDocumentDto(
    int Id,
    int BranchId,
    string TypeDoc,
    string SerieDoc,
    int NumberDoc,
    string Status,
    string? Observations
);