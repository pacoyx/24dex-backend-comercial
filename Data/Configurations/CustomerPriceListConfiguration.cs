using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerPriceListConfiguration : IEntityTypeConfiguration<CustomerPriceList>
{
    public void Configure(EntityTypeBuilder<CustomerPriceList> builder)
    {
        builder.ToTable("CustomerPriceLists");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

        builder.HasIndex(x => new { x.CustomerId, x.Version }).IsUnique();

        // Garantiza que un cliente no pueda tener mas de una tarifa activa a la vez.
        builder.HasIndex(x => x.CustomerId)
            .IsUnique()
            .HasFilter("[IsActive] = 1")
            .HasDatabaseName("IX_CustomerPriceLists_CustomerId_ActiveOnly");

        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Details)
            .WithOne(x => x.CustomerPriceList)
            .HasForeignKey(x => x.CustomerPriceListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
