public record CreateEmployeeDocumentRequestDto
{
    public int DocumentTypeId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public record EmployeeDocumentResponseDto
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentTypeName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
