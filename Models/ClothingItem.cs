using System.ComponentModel.DataAnnotations.Schema;
public class ClothingItem
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public string Status { get; set; } = "";
    public string TypRef { get; set; } = "";
    public int UserRef { get; set; }
}