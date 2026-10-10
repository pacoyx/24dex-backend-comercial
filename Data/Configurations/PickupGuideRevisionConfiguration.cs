using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PickupGuideRevisionConfiguration : IEntityTypeConfiguration<PickupGuideRevision>
{
    public void Configure(EntityTypeBuilder<PickupGuideRevision> builder)
    {
        builder.ToTable("PickupGuideRevisions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Observation).HasMaxLength(500);

        builder.HasIndex(x => x.PickupGuideId);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.DetailRevisions)
            .WithOne(x => x.PickupGuideRevision)
            .HasForeignKey(x => x.PickupGuideRevisionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
