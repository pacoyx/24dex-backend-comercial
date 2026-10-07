using Microsoft.EntityFrameworkCore;

public static class GetPriceListEndpoint
{
    public static void MapGetPriceList(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}", async (long id, RecepcionDbContext db) =>
        {
            var priceList = await db.CustomerPriceLists
                .AsNoTracking()
                .Include(x => x.Details).ThenInclude(d => d.ServiceItem)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (priceList is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new ApiResponse<CustomerPriceListResponse>
            {
                Success = true,
                Message = "Tarifa obtenida correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = CustomerPriceListsEndpoints.ToResponse(priceList)
            });
        })
        .WithName("GetCustomerPriceList")
        .Produces<ApiResponse<CustomerPriceListResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
