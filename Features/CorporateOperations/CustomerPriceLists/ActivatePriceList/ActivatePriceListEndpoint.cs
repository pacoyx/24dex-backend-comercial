using Microsoft.EntityFrameworkCore;

public static class ActivatePriceListEndpoint
{
    public static void MapActivatePriceList(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/activate", async (long id, RecepcionDbContext db) =>
        {
            var priceList = await db.CustomerPriceLists.FirstOrDefaultAsync(x => x.Id == id);
            if (priceList is null)
            {
                return Results.NotFound("Tarifa no encontrada.");
            }

            if (!priceList.IsActive)
            {
                var currentlyActive = await db.CustomerPriceLists
                    .Where(x => x.CustomerId == priceList.CustomerId && x.IsActive)
                    .ToListAsync();
                foreach (var active in currentlyActive)
                {
                    active.IsActive = false;
                }

                priceList.IsActive = true;
                await db.SaveChangesAsync();
            }

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Tarifa activada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { priceList.Id, priceList.IsActive }
            });
        })
        .WithName("ActivateCustomerPriceList")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
