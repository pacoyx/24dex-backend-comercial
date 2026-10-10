public class PickupGuideAttachment
{
    public long Id { get; set; }

    public long PickupGuideId { get; set; }

    public PickupGuide PickupGuide { get; set; } = null!;

    public PickupGuideAttachmentType AttachmentType { get; set; }

    public string OriginalFileName { get; set; } = string.Empty;

    public string StoredFileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public bool IsCurrent { get; set; } = true;

    public DateTime CreatedAt { get; set; }
}
