using Microsoft.EntityFrameworkCore;

public class PaymentMethodSeeder : IApplicationSeeder
{
    private static readonly string[] Names =
    {
        "Efectivo", "Transferencia Bancaria", "Tarjeta de Débito",
        "Tarjeta de Crédito", "Yape/Plin", "Otro"
    };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var existingNames = await context.PaymentMethods
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var missingNames = Names.Except(existingNames);

        foreach (var name in missingNames)
        {
            context.PaymentMethods.Add(new PaymentMethod { Name = name, IsActive = true });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
