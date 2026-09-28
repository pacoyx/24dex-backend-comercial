public record CreateExpenseAttachmentRequest(
    string FileName,
    string OriginalFileName,
    string BlobUrl
);

public record ExpenseAttachmentResponse(
    long Id,
    long ExpenseId,
    string FileName,
    string OriginalFileName,
    string BlobUrl,
    DateTime CreatedAt
);
