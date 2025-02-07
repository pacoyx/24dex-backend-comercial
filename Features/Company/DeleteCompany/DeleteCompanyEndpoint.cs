using Microsoft.AspNetCore.Http.HttpResults;

public static class DeleteCompany
{
    public static void MapDeleteCompany(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (int id, IDeleteCompanyService companyService) =>
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


            await companyService.DeleteCompany(id);

            var response = new ApiResponse<string>()
            {
                Data = "",
                Message = "Company deleted successfully",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }
}