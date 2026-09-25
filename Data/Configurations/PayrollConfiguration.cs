using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
{
    public void Configure(EntityTypeBuilder<Payroll> builder)
    {
        builder.ToTable("Payrolls");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GrossAmount).HasPrecision(18, 2);
        builder.Property(x => x.Discounts).HasPrecision(18, 2);
        builder.Property(x => x.NetAmount).HasPrecision(18, 2);

        builder.HasIndex(x => new { x.PayrollPeriodId, x.EmployeeId }).IsUnique();

        builder.HasOne(x => x.PayrollPeriod)
            .WithMany()
            .HasForeignKey(x => x.PayrollPeriodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.PayrollDetails)
            .WithOne(x => x.Payroll)
            .HasForeignKey(x => x.PayrollId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
