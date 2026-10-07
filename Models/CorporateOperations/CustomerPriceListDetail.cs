using System.ComponentModel.DataAnnotations.Schema;
using DexterCompany.Models;

public class CustomerPriceListDetail
{
    public long Id { get; set; }

    public long CustomerPriceListId { get; set; }

    public CustomerPriceList CustomerPriceList { get; set; } = null!;

    public int ServiceItemId { get; set; }

    public ProdService ServiceItem { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
}
