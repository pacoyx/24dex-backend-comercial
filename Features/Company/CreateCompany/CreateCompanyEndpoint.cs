public static class CreateCompany
{
    public static void MapCreateCompany(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateCompanyDto companyDto, ICreateCompanyService companyService) =>
        {
            if (companyDto == null)
            {
                return Results.BadRequest();
            }

            if (companyDto.usuarioId == 0)
            {
                return Results.BadRequest();
            }

            var existUser = await companyService.ExistUser(companyDto.usuarioId);
            if (!existUser)
            {
                var responseVal = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "User not found",
                    StatusCode = 200,
                    Success = false
                };
                return Results.Ok(responseVal);
            }

            var responseCompany = await companyService.CreateCompany(companyDto);

            var response = new ApiResponse<string>()
            {
                Data = responseCompany.Id.ToString(),
                Message = "User created successfully",
                StatusCode = 201,
                Success = true
            };
            return Results.Ok(response);
        });
    }
}