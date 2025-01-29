public static class ServicesDiExtensions{
    
    public static void AddServicesDi(this IServiceCollection services)
    {
        services.AddScoped<IGetServicesAccessFastService, GetServicesAccessFastService>();
        services.AddScoped<ICreateUserService, CreateUserService>();
        services.AddScoped<IDeleteUserService, DeleteUserService>();
        services.AddScoped<IGetUsersService, GetUsersService>();
        services.AddScoped<IGetUserService, GetUserService>();
        services.AddScoped<IUpdateUserService, UpdateUserService>();

        services.AddScoped<IEncryptService, EncryptService>();
    }
}