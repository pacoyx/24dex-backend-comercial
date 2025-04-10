using System.ComponentModel.DataAnnotations;

public class Supplier{
    public int Id { get; set; }
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(20)]
    public string Ruc { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Phone { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(1)]
    public string Status { get; set; } = "A"; // e.g., "Active", "Inactive"
    public List<Product>? Products { get; set; } // Navigation property for related products
}