using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeSalaryHistoryConfiguration : IEntityTypeConfiguration<EmployeeSalaryHistory>
{
    public void Configure(EntityTypeBuilder<EmployeeSalaryHistory> builder)
    {
        builder.ToTable("EmployeeSalaryHistories");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Salary).HasPrecision(18, 2);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
