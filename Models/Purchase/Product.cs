using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public int Stock { get; set; }    
    public int UnitMeasurementId { get; set; }
    public int CategoryProdId { get; set; }
    [MaxLength(1)]
    public string Status { get; set; } = "A";

    //navegacion properties
    public virtual UnitMeasurement? UnitMeasurement { get; set; }
    public virtual CategoryProd? CategoryProduct { get; set; } = null!;
}
