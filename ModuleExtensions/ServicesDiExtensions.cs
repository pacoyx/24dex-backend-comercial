public static class ServicesDiExtensions
{

    public static void AddServicesDi(this IServiceCollection services)
    {
        services.AddScoped<IGetServicesAccessFastService, GetServicesAccessFastService>();
        services.AddScoped<ICreateUserService, CreateUserService>();
        services.AddScoped<IDeleteUserService, DeleteUserService>();
        services.AddScoped<IGetUsersService, GetUsersService>();
        services.AddScoped<IGetUserService, GetUserService>();
        services.AddScoped<IUpdateUserService, UpdateUserService>();

        services.AddScoped<ICreateCompanyService, CreateCompanyService>();
        services.AddScoped<IDeleteCompanyService, DeleteCompanyService>();
        services.AddScoped<IGetCompaniesService, GetCompaniesService>();
        services.AddScoped<IGetCompanyService, GetCompanyService>();
        services.AddScoped<IUpdateCompanyService, UpdateCompanyService>();
        services.AddScoped<IGetCompanyByUserService, GetCompanyByUserService>();

        services.AddScoped<IEncryptService, EncryptService>();

        services.AddScoped<ICreateUnitMeasurementService, CreateUnitMeasurementService>();
        services.AddScoped<IGetUnitMeasurementsService, GetUnitMeasurementsService>();
        services.AddScoped<IGetCategoriesProdService, GetCategoriesProdService>();
        services.AddScoped<ICreateCategoryProdService, CreateCategoryProdService>();
        
        services.AddScoped<IGetProductsService, GetProductsService>();
        services.AddScoped<ICreateProductService, CreateProductService>();
        services.AddScoped<IUpdateProductService, UpdateProductService>();

        services.AddScoped<ICreateSupplierService, CreateSupplierService>();
        services.AddScoped<IUpdateSupplierService, UpdateSupplierService>();
        services.AddScoped<IGetSupplierService, GetSupplierService>();
        
        services.AddScoped<ICreatePurchaseInvoiceService, CreatePurchaseInvoiceService>();
        services.AddScoped<IGetPuchasesInvoiceService, GetPuchasesInvoiceService>();    
        services.AddScoped<IGetInvoicesByParamsService, GetInvoicesByParamsService>();
    }
}