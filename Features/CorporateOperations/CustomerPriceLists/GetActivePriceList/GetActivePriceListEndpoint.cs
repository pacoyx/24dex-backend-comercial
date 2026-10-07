using Microsoft.EntityFrameworkCore;

public static class GetActivePriceListEndpoint
{
    public static void MapGetActivePriceList(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}/price-lists/active", async (int id, RecepcionDbContext db) =>
        {
            var priceList = await db.CustomerPriceLists
                .AsNoTracking()
                .Include(x => x.Details).ThenInclude(d => d.ServiceItem)
                .FirstOrDefaultAsync(x => x.CustomerId == id && x.IsActive);

            if (priceList is null)
            {
                return Results.NotFound(new ApiResponse<CustomerPriceListResponse>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "El cliente no tiene una tarifa activa."
                });
            }

            return Results.Ok(new ApiResponse<CustomerPriceListResponse>
            {
                Success = true,
                Message = "Tarifa activa obtenida correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = CustomerPriceListsEndpoints.ToResponse(priceList)
            });
        })
        .WithName("GetActiveCustomerPriceList")
        .Produces<ApiResponse<CustomerPriceListResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
