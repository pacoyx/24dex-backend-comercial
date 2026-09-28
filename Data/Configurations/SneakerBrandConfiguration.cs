using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SneakerBrandConfiguration : IEntityTypeConfiguration<SneakerBrand>
{
    public void Configure(EntityTypeBuilder<SneakerBrand> builder)
    {
        builder.ToTable("SneakerBrands");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
    }
}
