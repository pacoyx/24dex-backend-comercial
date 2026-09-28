using Microsoft.EntityFrameworkCore;

public class PermissionSeeder : IApplicationSeeder
{
    private static readonly (string Code, string Description)[] Permissions =
    {
        (PermissionCodes.Dashboard, "Acceso al dashboard general"),
        (PermissionCodes.Reception, "Acceso al modulo de Recepcion"),
        (PermissionCodes.Customers, "Acceso al modulo de Clientes"),
        (PermissionCodes.Invoices, "Acceso al modulo de Facturacion"),
        (PermissionCodes.Purchases, "Acceso al modulo de Compras"),
        (PermissionCodes.Expenses, "Acceso al modulo de Gastos"),
        (PermissionCodes.Employees, "Acceso al modulo de Recursos Humanos"),
        (PermissionCodes.Payroll, "Acceso al modulo de Planillas"),
        (PermissionCodes.Suppliers, "Acceso al modulo de Proveedores"),
        (PermissionCodes.Reports, "Acceso al modulo de Reportes"),
        (PermissionCodes.Settings, "Acceso a la configuracion del sistema")
    };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var existingCodes = await context.Permissions
            .Select(x => x.Code)
            .ToListAsync(cancellationToken);

        var missing = Permissions.Where(p => !existingCodes.Contains(p.Code));

        foreach (var permission in missing)
        {
            context.Permissions.Add(new Permission
            {
                Code = permission.Code,
                Description = permission.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
