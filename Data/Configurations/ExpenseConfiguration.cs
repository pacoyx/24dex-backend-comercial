using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount).HasPrecision(18, 2);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.DocumentNumber).HasMaxLength(50);
        builder.Property(x => x.SupplierName).HasMaxLength(150);
        builder.Property(x => x.Observation).HasMaxLength(500);

        builder.HasIndex(x => x.ExpenseDate);
        builder.HasIndex(x => x.BranchSalesId);

        builder.HasOne(x => x.BranchSales)
            .WithMany()
            .HasForeignKey(x => x.BranchSalesId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExpenseCategory)
            .WithMany()
            .HasForeignKey(x => x.ExpenseCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Attachments)
            .WithOne(x => x.Expense)
            .HasForeignKey(x => x.ExpenseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
