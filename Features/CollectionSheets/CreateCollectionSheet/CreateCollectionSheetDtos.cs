public record CreateCollectionSheetRequest(
    int BranchId,
    string Number,
    string? Remarks
);

public record CollectionSheetResponse(
    long Id,
    int BranchId,
    string Number,
    DateTime OpenDate,
    DateTime? CloseDate,
    decimal TotalAmount,
    CollectionSheetStatus Status,
    string? Remarks
);
