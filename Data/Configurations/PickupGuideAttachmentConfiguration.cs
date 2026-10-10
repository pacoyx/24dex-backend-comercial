using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PickupGuideAttachmentConfiguration : IEntityTypeConfiguration<PickupGuideAttachment>
{
    public void Configure(EntityTypeBuilder<PickupGuideAttachment> builder)
    {
        builder.ToTable("PickupGuideAttachments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalFileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.StoredFileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.FilePath).IsRequired().HasMaxLength(500);
        builder.Property(x => x.ContentType).IsRequired().HasMaxLength(100);

        builder.HasIndex(x => new { x.PickupGuideId, x.AttachmentType, x.IsCurrent });
    }
}
