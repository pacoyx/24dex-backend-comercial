using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CollectionSheetConfiguration : IEntityTypeConfiguration<CollectionSheet>
{
    public void Configure(EntityTypeBuilder<CollectionSheet> builder)
    {
        builder.ToTable("CollectionSheets");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Remarks).HasMaxLength(500);

        builder.Property(x => x.TotalAmount).HasPrecision(18, 2);

        builder.HasIndex(x => x.Number).IsUnique();
    }
}
