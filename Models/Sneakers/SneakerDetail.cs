
public class SneakerDetail
{
    public long Id { get; set; }

    public int WorkGuideDetailId { get; set; }

    public int? SneakerBrandId { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public bool HasLaces { get; set; }

    public bool HasInsoles { get; set; }

    public string? Observations { get; set; }

    public DateTime CreatedAt { get; set; }

    public WorkGuideDetail WorkGuideDetail { get; set; } = null!;

    public SneakerBrand? Brand { get; set; }
}
