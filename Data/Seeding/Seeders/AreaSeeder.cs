using Microsoft.EntityFrameworkCore;

public class AreaSeeder : IApplicationSeeder
{
    private static readonly string[] Names =
    {
        "Recepción", "Producción", "Administración", "Despacho"
    };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var existingNames = await context.Areas
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var missingNames = Names.Except(existingNames);

        foreach (var name in missingNames)
        {
            context.Areas.Add(new Area { Name = name, IsActive = true });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
