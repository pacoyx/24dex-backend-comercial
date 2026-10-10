using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerUserTokenConfiguration : IEntityTypeConfiguration<CustomerUserToken>
{
    public void Configure(EntityTypeBuilder<CustomerUserToken> builder)
    {
        builder.ToTable("CustomerUserTokens");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Purpose).IsRequired().HasMaxLength(30);

        builder.HasIndex(x => x.Token).IsUnique();
        builder.HasIndex(x => new { x.CustomerUserId, x.Purpose, x.IsUsed });

        builder.HasOne<CustomerUser>()
            .WithMany()
            .HasForeignKey(x => x.CustomerUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
