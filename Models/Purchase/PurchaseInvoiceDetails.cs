using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PurchaseInvoiceDetails
{
    public int Id { get; set; }
    public int PurchaseInvoiceId { get; set; }
    public int ProductId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }
    [MaxLength(200)]
    public string Comments { get; set; } = string.Empty;
    [MaxLength(1)]
    public string Status { get; set; } = "A"; // e.g., "Active", "Inactive"

    // Navigation properties
    public PurchaseInvoice? PurchaseInvoice { get; set; }
    public Product? Product { get; set; }
}