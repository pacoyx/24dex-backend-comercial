using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Positions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasData(
            new Position { Id = 1, Name = "Recepcionista", IsActive = true },
            new Position { Id = 2, Name = "Operario", IsActive = true },
            new Position { Id = 3, Name = "Supervisor", IsActive = true },
            new Position { Id = 4, Name = "Administrador", IsActive = true },
            new Position { Id = 5, Name = "Motorizado", IsActive = true }
        );
    }
}
