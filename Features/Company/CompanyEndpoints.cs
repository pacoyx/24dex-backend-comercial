public static class CompanyEndpoints
{
    public static void MapCompany(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/company");
        group.MapCreateCompany();
        group.MapUpdateCompany();
        group.MapDeleteCompany();
        group.MapGetCompanies();
        group.MapGetCompany();
        group.MapGetCompanyByUser();
    }
}