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
    [MaxLength(4)]
    public string CodeSunat { get; set; } = string.Empty; // SUNAT code for the unit of measurement 
    [MaxLength(20)]
    public string Abbreviation { get; set; } = string.Empty; // Abbreviation for the unit of measurement
    [MaxLength(5)]
    public string Symbol { get; set; } = string.Empty; // Symbol for the unit of measurement
    
    public List<Product>? Products { get; set; } // Navigation property for related products
}