public static class ServicesDiExtensions{
    
    public static void AddServicesDi(this IServiceCollection services)
    {
        services.AddScoped<IGetServicesAccessFastService, GetServicesAccessFastService>();
    }
}