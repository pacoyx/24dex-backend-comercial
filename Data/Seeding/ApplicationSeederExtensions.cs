public static class ApplicationSeederExtensions
{
    public static async Task RunSeedersAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RecepcionDbContext>();
        var seeders = scope.ServiceProvider.GetServices<IApplicationSeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(context);
        }
    }
}
