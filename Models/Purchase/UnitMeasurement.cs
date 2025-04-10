using System.ComponentModel.DataAnnotations;

public class UnitMeasurement
{
    public int Id { get; set; }
    [MaxLength(5)]    
    public string CodeUm { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(1)]
    public string Status { get; set; } = "A"; // e.g., "Active", "Inactive"
    public List<Product>? Products { get; set; } // Navigation property for related products
}