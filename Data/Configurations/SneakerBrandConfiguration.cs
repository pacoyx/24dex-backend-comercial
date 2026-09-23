using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SneakerBrandConfiguration : IEntityTypeConfiguration<SneakerBrand>
{
    public void Configure(EntityTypeBuilder<SneakerBrand> builder)
    {
        builder.ToTable("SneakerBrands");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasData(
            new SneakerBrand { Id = 1, Name = "Nike", IsActive = true },
            new SneakerBrand { Id = 2, Name = "Adidas", IsActive = true },
            new SneakerBrand { Id = 3, Name = "Reebok", IsActive = true },
            new SneakerBrand { Id = 4, Name = "Puma", IsActive = true },
            new SneakerBrand { Id = 5, Name = "New Balance", IsActive = true },
            new SneakerBrand { Id = 6, Name = "Converse", IsActive = true },
            new SneakerBrand { Id = 7, Name = "Vans", IsActive = true },
            new SneakerBrand { Id = 8, Name = "Asics", IsActive = true },
            new SneakerBrand { Id = 9, Name = "Fila", IsActive = true },
            new SneakerBrand { Id = 10, Name = "Under Armour", IsActive = true }
        );
    }
}
