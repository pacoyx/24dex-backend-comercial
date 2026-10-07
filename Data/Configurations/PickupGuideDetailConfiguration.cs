using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PickupGuideDetailConfiguration : IEntityTypeConfiguration<PickupGuideDetail>
{
    public void Configure(EntityTypeBuilder<PickupGuideDetail> builder)
    {
        builder.ToTable("PickupGuideDetails");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).HasPrecision(18, 2);
        builder.Property(x => x.Weight).HasPrecision(18, 2);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
        builder.Property(x => x.Total).HasPrecision(18, 2);
        builder.Property(x => x.Observation).HasMaxLength(500);

        builder.HasOne(x => x.ServiceItem)
            .WithMany()
            .HasForeignKey(x => x.ServiceItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
