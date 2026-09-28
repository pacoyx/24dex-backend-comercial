using Microsoft.EntityFrameworkCore;

public class SneakerBrandSeeder : IApplicationSeeder
{
    private static readonly string[] Names =
    {
        "Nike", "Adidas", "Reebok", "Puma", "New Balance",
        "Converse", "Vans", "Asics", "Fila", "Under Armour"
    };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var existingNames = await context.SneakerBrands
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var missingNames = Names.Except(existingNames);

        foreach (var name in missingNames)
        {
            context.SneakerBrands.Add(new SneakerBrand { Name = name, IsActive = true });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
