using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Status).IsRequired().HasMaxLength(10);
        builder.Property(u => u.RefreshToken).HasDefaultValue("");
        builder.HasMany(u => u.BrachSalesUsers).WithOne(bsu => bsu.User).HasForeignKey(bsu => bsu.UserId);
    }
}