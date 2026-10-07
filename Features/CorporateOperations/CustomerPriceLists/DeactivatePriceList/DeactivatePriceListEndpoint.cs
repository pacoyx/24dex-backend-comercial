using Microsoft.EntityFrameworkCore;

public static class DeactivatePriceListEndpoint
{
    public static void MapDeactivatePriceList(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/deactivate", async (long id, RecepcionDbContext db) =>
        {
            var priceList = await db.CustomerPriceLists.FirstOrDefaultAsync(x => x.Id == id);
            if (priceList is null)
            {
                return Results.NotFound("Tarifa no encontrada.");
            }

            priceList.IsActive = false;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Tarifa desactivada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { priceList.Id, priceList.IsActive }
            });
        })
        .WithName("DeactivateCustomerPriceList")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
