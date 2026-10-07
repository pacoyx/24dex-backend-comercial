using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PickupGuide
{
    public long Id { get; set; }

    [MaxLength(40)]
    public string GuideNumber { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public int BranchSalesId { get; set; }

    public BranchSales BranchSales { get; set; } = null!;

    public DateTime PickupDate { get; set; }

    public PickupGuideStatus Status { get; set; } = PickupGuideStatus.Draft;

    [MaxLength(500)]
    public string? Observation { get; set; }

    public long CustomerPriceListId { get; set; }

    public CustomerPriceList CustomerPriceList { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ICollection<PickupGuideDetail> Details { get; set; } = new List<PickupGuideDetail>();
}
