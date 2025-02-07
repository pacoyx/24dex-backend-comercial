public static class UpdateCompanyEndpoint
{
    public static void MapUpdateCompany(this IEndpointRouteBuilder app)
    {

        app.MapPut("/", async (UpdateCompanyDto companyDto, IUpdateCompanyService updateService) =>
        {
            if (companyDto == null)
            {
                return Results.BadRequest();
            }

            var company = await updateService.UpdateCompany(companyDto);
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

            var response = new ApiResponse<string>()
            {
                Data = company.Id.ToString(),
                Message = "Company updated successfully",
                StatusCode = 201,
                Success = true
            };
            return Results.Ok(response);

        });
    }
}