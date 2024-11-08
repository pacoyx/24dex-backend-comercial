using Microsoft.EntityFrameworkCore;

public static class SearchCustomerEndpoint
{
    public static void MapSearchCustomer(this IEndpointRouteBuilder app)
    {
        app.MapGet("/search/{name}", async (RecepcionDbContext context, string name) =>
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Results.BadRequest("The name is required");
            }

            var customers = await context.Customers.AsNoTracking()
                .Where(x => x.FirtsName.Contains(name) || x.LastName.Contains(name))
                .Select(x => new ResponseSearchCustomerDto(
                    x.Id,
                    x.FirtsName.Substring(0, 1).ToUpper() + x.LastName.Substring(0, 1).ToUpper() + x.Id.ToString(),
                    x.FirtsName,
                    x.LastName,                    
                    x.Phone
                )).ToListAsync();

            var response = new ApiResponse<List<ResponseSearchCustomerDto>>(){
                Data = customers,
                Message = "Customers found",
                Success = true,
                StatusCode = 200                
            };

            return Results.Ok(response);
        });
    }
}