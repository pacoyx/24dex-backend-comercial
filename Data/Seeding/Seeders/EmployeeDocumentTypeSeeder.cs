using Microsoft.EntityFrameworkCore;

public class EmployeeDocumentTypeSeeder : IApplicationSeeder
{
    private static readonly string[] Names =
    {
        "DNI Frente", "DNI Reverso", "Contrato", "CV", "Licencia",
        "Antecedentes", "Certificado Médico", "Otro"
    };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var existingNames = await context.EmployeeDocumentTypes
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var missingNames = Names.Except(existingNames);

        foreach (var name in missingNames)
        {
            context.EmployeeDocumentTypes.Add(new EmployeeDocumentType { Name = name });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
