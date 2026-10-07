public class CustomerPriceList
{
    public long Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public int Version { get; set; }

    public DateTime EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<CustomerPriceListDetail> Details { get; set; }
        = new List<CustomerPriceListDetail>();
}
