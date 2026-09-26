using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class CompanySearchEndpoint
{
    public static void MapCompanySearch(this IEndpointRouteBuilder app)
    {
        app.MapGet("/company-search", async (
            [FromQuery] string? ruc,
            [FromQuery] string? businessName,
            [FromQuery] string? commercialName,
            RecepcionDbContext db) =>
        {
            var query = db.CustomerBillingProfiles
                .AsNoTracking()
                .Where(x => x.Customer.CustomerType == CustomerType.Company)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(ruc))
            {
                query = query.Where(x => x.TaxId.Contains(ruc));
            }

            if (!string.IsNullOrWhiteSpace(businessName))
            {
                query = query.Where(x => x.BusinessName.Contains(businessName));
            }

            if (!string.IsNullOrWhiteSpace(commercialName))
            {
                query = query.Where(x => x.CommercialName != null && x.CommercialName.Contains(commercialName));
            }

            var results = await query
                .OrderBy(x => x.BusinessName)
                .Select(x => new CompanySearchResponseDto(x.CustomerId, x.BusinessName, x.CommercialName, x.TaxId))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<CompanySearchResponseDto>>
            {
                Success = true,
                Message = "Empresas encontradas.",
                StatusCode = StatusCodes.Status200OK,
                Data = results
            });
        })
        .WithName("SearchCompanyCustomers")
        .Produces<ApiResponse<List<CompanySearchResponseDto>>>(StatusCodes.Status200OK)
        .RequireAuthorization();
    }
}
