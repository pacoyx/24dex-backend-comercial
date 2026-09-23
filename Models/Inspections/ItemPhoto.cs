
public class ItemPhoto
{
    public long Id { get; set; }

    public long ItemInspectionId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string? OriginalFileName { get; set; }

    public string BlobUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public ItemInspection ItemInspection { get; set; } = null!;
}
