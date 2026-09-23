using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SneakerDetailConfiguration : IEntityTypeConfiguration<SneakerDetail>
{
    public void Configure(EntityTypeBuilder<SneakerDetail> builder)
    {
        builder.ToTable("SneakerDetails");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PrimaryColor).HasMaxLength(50);
        builder.Property(x => x.SecondaryColor).HasMaxLength(50);
        builder.Property(x => x.Observations).HasMaxLength(500);

        builder.HasIndex(x => x.WorkGuideDetailId).IsUnique();

        builder.HasOne(x => x.WorkGuideDetail)
            .WithOne(x => x.SneakerDetail)
            .HasForeignKey<SneakerDetail>(x => x.WorkGuideDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.SneakerBrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
