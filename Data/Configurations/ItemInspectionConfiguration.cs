using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ItemInspectionConfiguration : IEntityTypeConfiguration<ItemInspection>
{
    public void Configure(EntityTypeBuilder<ItemInspection> builder)
    {
        builder.ToTable("ItemInspections");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Notes).HasMaxLength(500);

        builder.HasIndex(x => x.WorkGuideDetailId).IsUnique();

        builder.HasOne(x => x.WorkGuideDetail)
            .WithOne(x => x.ItemInspection)
            .HasForeignKey<ItemInspection>(x => x.WorkGuideDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Photos)
            .WithOne(x => x.ItemInspection)
            .HasForeignKey(x => x.ItemInspectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
