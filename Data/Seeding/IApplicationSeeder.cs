public interface IApplicationSeeder
{
    Task SeedAsync(RecepcionDbContext context, CancellationToken cancellationToken = default);
}
