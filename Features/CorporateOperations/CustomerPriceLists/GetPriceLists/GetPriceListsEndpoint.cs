using Microsoft.EntityFrameworkCore;

public static class GetPriceListsEndpoint
{
    public static void MapGetPriceLists(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/price-lists", async (int id, RecepcionDbContext db) =>
        {
            var priceLists = await db.CustomerPriceLists
                .AsNoTracking()
                .Where(x => x.CustomerId == id)
                .OrderByDescending(x => x.Version)
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<CustomerPriceListSummaryResponse>>
            {
                Success = true,
                Message = "Listado de tarifas del cliente.",
                StatusCode = StatusCodes.Status200OK,
                Data = priceLists.Select(CustomerPriceListsEndpoints.ToSummaryResponse).ToList()
            });
        })
        .WithName("GetCustomerPriceLists")
        .Produces<ApiResponse<List<CustomerPriceListSummaryResponse>>>(StatusCodes.Status200OK);
    }
}
