using Microsoft.EntityFrameworkCore;

public class ExpenseCategorySeeder : IApplicationSeeder
{
    private static readonly string[] Names =
    {
        "Agua", "Luz", "Gas", "Internet", "Alquiler", "Mantenimiento",
        "Papelería", "Limpieza", "Licencias", "Hosting", "Dominio",
        "Servicios Profesionales", "Seguros", "Combustible", "Telefonía", "Otros"
    };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var existingNames = await context.ExpenseCategories
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var missingNames = Names.Except(existingNames);

        foreach (var name in missingNames)
        {
            context.ExpenseCategories.Add(new ExpenseCategory { Name = name, IsActive = true, CreatedAt = DateTime.UtcNow });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
