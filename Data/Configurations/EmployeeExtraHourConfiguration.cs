using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeExtraHourConfiguration : IEntityTypeConfiguration<EmployeeExtraHour>
{
    public void Configure(EntityTypeBuilder<EmployeeExtraHour> builder)
    {
        builder.ToTable("EmployeeExtraHours");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Hours).HasPrecision(9, 2);
        builder.Property(x => x.Observation).HasMaxLength(500);

        builder.HasIndex(x => new { x.EmployeeId, x.Date });

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
