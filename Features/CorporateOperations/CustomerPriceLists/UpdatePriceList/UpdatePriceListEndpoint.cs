using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class UpdatePriceListEndpoint
{
    public static void MapUpdatePriceList(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:long}", async (
            long id,
            UpdatePriceListRequest request,
            IValidator<UpdatePriceListRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var priceList = await db.CustomerPriceLists
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (priceList is null)
            {
                return Results.NotFound("Tarifa no encontrada.");
            }

            var inUse = await db.PickupGuides.AnyAsync(x => x.CustomerPriceListId == id);
            if (inUse)
            {
                return Results.BadRequest(new[] { "No se puede modificar una tarifa que ya fue utilizada en guías de recojo. Cree una nueva versión." });
            }

            var serviceItemIds = request.Details.Select(d => d.ServiceItemId).ToList();
            var existingServiceItems = await db.ProdServices
                .Where(x => serviceItemIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();

            var missingServiceItems = serviceItemIds.Except(existingServiceItems).ToList();
            if (missingServiceItems.Count > 0)
            {
                return Results.BadRequest(new[] { $"Los siguientes servicios no existen: {string.Join(", ", missingServiceItems)}" });
            }

            priceList.Name = request.Name;
            priceList.EffectiveFrom = request.EffectiveFrom;
            priceList.EffectiveTo = request.EffectiveTo;

            db.CustomerPriceListDetails.RemoveRange(priceList.Details);
            priceList.Details = request.Details.Select(d => new CustomerPriceListDetail
            {
                ServiceItemId = d.ServiceItemId,
                UnitPrice = d.UnitPrice
            }).ToList();

            await db.SaveChangesAsync();

            await db.Entry(priceList).Collection(x => x.Details).Query()
                .Include(x => x.ServiceItem)
                .LoadAsync();

            return Results.Ok(new ApiResponse<CustomerPriceListResponse>
            {
                Success = true,
                Message = "Tarifa actualizada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = CustomerPriceListsEndpoints.ToResponse(priceList)
            });
        })
        .WithName("UpdateCustomerPriceList")
        .Produces<ApiResponse<CustomerPriceListResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
