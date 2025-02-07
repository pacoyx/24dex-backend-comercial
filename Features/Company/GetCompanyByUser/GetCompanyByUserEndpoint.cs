public static class GetCompanyByUserEndpoint
{
    public static void MapGetCompanyByUser(this IEndpointRouteBuilder app)
    {
        app.MapGet("/user/{userId}", async (int userId, IGetCompanyByUserService companyService) =>
        {
            if (userId == 0)
            {
                return Results.BadRequest();
            }

            var company = await companyService.GetCompanyByUser(userId);
            if (company == null)
            {
                var api = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "Company not found,user not have company",
                    StatusCode = 404,
                    Success = false
                };
                return Results.Ok(api);
            }

            var responseCompany = new GetCompanyByUserResponseDto(
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
                company.Instagram ?? string.Empty,
                company.Status                
            );

            var response = new ApiResponse<GetCompanyByUserResponseDto>()
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