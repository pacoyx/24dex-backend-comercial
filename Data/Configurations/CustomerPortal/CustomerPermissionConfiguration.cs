using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerPermissionConfiguration : IEntityTypeConfiguration<CustomerPermission>
{
    public void Configure(EntityTypeBuilder<CustomerPermission> builder)
    {
        builder.ToTable("CustomerPermissions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(300);

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
