using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PickupGuideConfiguration : IEntityTypeConfiguration<PickupGuide>
{
    public void Configure(EntityTypeBuilder<PickupGuide> builder)
    {
        builder.ToTable("PickupGuides");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GuideNumber).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Observation).HasMaxLength(500);
        builder.Property(x => x.TotalAmount).HasPrecision(18, 2);
        builder.Property(x => x.Status).HasDefaultValue(PickupGuideStatus.Draft);

        builder.HasIndex(x => x.GuideNumber).IsUnique();
        builder.HasIndex(x => new { x.CustomerId, x.Status });

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BranchSales)
            .WithMany()
            .HasForeignKey(x => x.BranchSalesId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CustomerPriceList)
            .WithMany()
            .HasForeignKey(x => x.CustomerPriceListId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Details)
            .WithOne(x => x.PickupGuide)
            .HasForeignKey(x => x.PickupGuideId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
