using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeDocumentTypeConfiguration : IEntityTypeConfiguration<EmployeeDocumentType>
{
    public void Configure(EntityTypeBuilder<EmployeeDocumentType> builder)
    {
        builder.ToTable("EmployeeDocumentTypes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasData(
            new EmployeeDocumentType { Id = 1, Name = "DNI Frente" },
            new EmployeeDocumentType { Id = 2, Name = "DNI Reverso" },
            new EmployeeDocumentType { Id = 3, Name = "Contrato" },
            new EmployeeDocumentType { Id = 4, Name = "CV" },
            new EmployeeDocumentType { Id = 5, Name = "Licencia" },
            new EmployeeDocumentType { Id = 6, Name = "Antecedentes" },
            new EmployeeDocumentType { Id = 7, Name = "Certificado Médico" },
            new EmployeeDocumentType { Id = 8, Name = "Otro" }
        );
    }
}
