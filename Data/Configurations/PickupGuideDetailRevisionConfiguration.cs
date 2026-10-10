using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PickupGuideDetailRevisionConfiguration : IEntityTypeConfiguration<PickupGuideDetailRevision>
{
    public void Configure(EntityTypeBuilder<PickupGuideDetailRevision> builder)
    {
        builder.ToTable("PickupGuideDetailRevisions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalQuantity).HasPrecision(18, 2);
        builder.Property(x => x.NewQuantity).HasPrecision(18, 2);
        builder.Property(x => x.Reason).IsRequired().HasMaxLength(500);

        builder.HasOne(x => x.PickupGuideDetail)
            .WithMany()
            .HasForeignKey(x => x.PickupGuideDetailId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
