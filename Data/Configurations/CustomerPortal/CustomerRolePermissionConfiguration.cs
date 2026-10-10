using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerRolePermissionConfiguration : IEntityTypeConfiguration<CustomerRolePermission>
{
    public void Configure(EntityTypeBuilder<CustomerRolePermission> builder)
    {
        builder.ToTable("CustomerRolePermissions");
        builder.HasKey(x => new { x.CustomerRoleId, x.CustomerPermissionId });

        builder.HasOne(x => x.CustomerRole)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.CustomerRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CustomerPermission)
            .WithMany()
            .HasForeignKey(x => x.CustomerPermissionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
