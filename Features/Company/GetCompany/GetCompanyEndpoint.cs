public static class GetCompanyEndpoint
{
    public static void MapGetCompany(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, IGetCompanyService companyService) =>
        {
            if (id == 0)
            {
                return Results.BadRequest();
            }

            var company = await companyService.GetCompany(id);
            if (company == null)
            {
                var api = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "Company not found",
                    StatusCode = 404,
                    Success = false
                };
                return Results.Ok(api);
            }

            var responseCompany = new GetCompanyDto(
                company.Id,
                company.NameComercial,
                company.Description ?? string.Empty,
                company.Address ?? string.Empty,
                company.Email ?? string.Empty,
                company.Phone ?? string.Empty,
                company.WebSite ?? string.Empty,
                company.UsuarioId,
                company.NameCompany,
                company.DocumentType,
                company.NumberType,
                company.Logo ?? string.Empty,
                company.Facebook ?? string.Empty,
                company.Twitter ?? string.Empty,
                company.Instagram ?? string.Empty
            );

            var response = new ApiResponse<GetCompanyDto>()
            {
                Data = responseCompany,
                Message = "Company found",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }
}