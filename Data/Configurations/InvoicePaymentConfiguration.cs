using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvoicePaymentConfiguration : IEntityTypeConfiguration<InvoicePayment>
{
    public void Configure(EntityTypeBuilder<InvoicePayment> builder)
    {
        builder.ToTable("InvoicePayments");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OperationNumber).HasMaxLength(50);
        builder.Property(x => x.Observation).HasMaxLength(500);

        builder.Property(x => x.Amount).HasPrecision(18, 2);

        builder.HasOne(x => x.PaymentMethod)
            .WithMany()
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CollectionSheet)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.CollectionSheetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.InvoiceId);
        builder.HasIndex(x => x.CollectionSheetId);
    }
}
