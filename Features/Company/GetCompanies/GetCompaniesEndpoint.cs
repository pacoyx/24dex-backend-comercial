public static class GetCompaniesEndpoint
{
    public static void MapGetCompanies(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (IGetCompaniesService companyService) =>
        {
            var companies = await companyService.GetCompanies();
            var responseApi = new ApiResponse<IEnumerable<GetCompaniesDto>>()
            {
                Data = companies,
                Message = "Companies listed successfully",
                Success = true,
                StatusCode = 200
            };
            return Results.Ok(companies);
        });
    }
}