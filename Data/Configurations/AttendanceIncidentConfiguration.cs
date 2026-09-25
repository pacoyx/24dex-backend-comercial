using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AttendanceIncidentConfiguration : IEntityTypeConfiguration<AttendanceIncident>
{
    public void Configure(EntityTypeBuilder<AttendanceIncident> builder)
    {
        builder.ToTable("AttendanceIncidents");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Hours).HasPrecision(9, 2);
        builder.Property(x => x.Observation).HasMaxLength(500);

        builder.HasIndex(x => new { x.EmployeeId, x.Date, x.Type });

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
