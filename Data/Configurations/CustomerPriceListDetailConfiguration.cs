using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerPriceListDetailConfiguration : IEntityTypeConfiguration<CustomerPriceListDetail>
{
    public void Configure(EntityTypeBuilder<CustomerPriceListDetail> builder)
    {
        builder.ToTable("CustomerPriceListDetails");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);

        builder.HasIndex(x => new { x.CustomerPriceListId, x.ServiceItemId }).IsUnique();

        builder.HasOne(x => x.ServiceItem)
            .WithMany()
            .HasForeignKey(x => x.ServiceItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
