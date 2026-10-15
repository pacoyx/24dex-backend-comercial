
public class ItemInspection
{
    public long Id { get; set; }

    public int WorkGuideDetailId { get; set; }

    public bool HasStains { get; set; }

    public bool HasTears { get; set; }

    public bool HasFading { get; set; }

    public bool HasMissingParts { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public WorkGuideDetail WorkGuideDetail { get; set; } = null!;
}
