using Microsoft.EntityFrameworkCore;

public class UserRoleBootstrapSeeder : IApplicationSeeder
{
    private static readonly string[] LegacyAdminAliases = { "admin", "administrador" };

    public async Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default)
    {
        var adminRole = await context.Roles
            .FirstOrDefaultAsync(x => x.Name == RoleNames.Administrador, cancellationToken);
        if (adminRole is null)
        {
            return;
        }

        var usersWithoutRoles = await context.Users
            .Where(u => !context.UserRoles.Any(ur => ur.UserId == u.Id))
            .Select(u => new { u.Id, u.Role })
            .ToListAsync(cancellationToken);

        foreach (var user in usersWithoutRoles)
        {
            var legacyRole = user.Role?.Trim().ToLowerInvariant();
            if (legacyRole is null || !LegacyAdminAliases.Contains(legacyRole))
            {
                continue;
            }

            context.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = adminRole.Id
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
