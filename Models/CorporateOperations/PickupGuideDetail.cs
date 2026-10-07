using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DexterCompany.Models;

public class PickupGuideDetail
{
    public long Id { get; set; }

    public long PickupGuideId { get; set; }

    public PickupGuide PickupGuide { get; set; } = null!;

    public int ServiceItemId { get; set; }

    public ProdService ServiceItem { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Weight { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    [MaxLength(500)]
    public string? Observation { get; set; }
}
