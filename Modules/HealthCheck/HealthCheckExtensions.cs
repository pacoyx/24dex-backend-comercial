using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddHealthChecks().AddSqlServer(connectionString, tags: new[] { "database" }); // Add SQL Server health check
        }
        services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "self" }); // Add self health check
        services.AddHealthChecksUI().AddInMemoryStorage(); // Add HealthCheck UI
        return services;
    }


}


