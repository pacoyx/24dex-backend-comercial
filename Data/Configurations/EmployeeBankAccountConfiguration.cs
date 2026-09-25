using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeBankAccountConfiguration : IEntityTypeConfiguration<EmployeeBankAccount>
{
    public void Configure(EntityTypeBuilder<EmployeeBankAccount> builder)
    {
        builder.ToTable("EmployeeBankAccounts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BankName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.AccountNumber).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Cci).HasMaxLength(30);
    }
}
