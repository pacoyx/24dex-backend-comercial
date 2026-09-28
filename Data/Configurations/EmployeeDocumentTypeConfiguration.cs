using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeDocumentTypeConfiguration : IEntityTypeConfiguration<EmployeeDocumentType>
{
    public void Configure(EntityTypeBuilder<EmployeeDocumentType> builder)
    {
        builder.ToTable("EmployeeDocumentTypes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}
