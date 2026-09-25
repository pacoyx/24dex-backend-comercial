using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable("Areas");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasData(
            new Area { Id = 1, Name = "Recepción", IsActive = true },
            new Area { Id = 2, Name = "Producción", IsActive = true },
            new Area { Id = 3, Name = "Administración", IsActive = true },
            new Area { Id = 4, Name = "Despacho", IsActive = true }
        );
    }
}
