using Microsoft.EntityFrameworkCore;

public class PositionSeeder : IApplicationSeeder
{
    private static readonly string[] Names =
    {
        "Recepcionista", "Operario", "Supervisor", "Administrador", "Motorizado"
    };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var existingNames = await context.Positions
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var missingNames = Names.Except(existingNames);

        foreach (var name in missingNames)
        {
            context.Positions.Add(new Position { Name = name, IsActive = true });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
