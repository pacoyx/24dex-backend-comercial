using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class UpdatePickupGuideEndpoint
{
    public static void MapUpdatePickupGuide(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:long}", async (
            long id,
            UpdatePickupGuideRequest request,
            IValidator<UpdatePickupGuideRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var guide = await db.PickupGuides
                .Include(x => x.Customer).ThenInclude(c => c.BillingProfile)
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (guide is null)
            {
                return Results.NotFound("Guía no encontrada.");
            }

            if (guide.Status is PickupGuideStatus.Invoiced or PickupGuideStatus.Cancelled)
            {
                return Results.BadRequest(new[] { $"No se puede modificar una guía en estado '{guide.Status}'." });
            }

            var branchExists = await db.BranchSales.AnyAsync(x => x.Id == request.BranchSalesId);
            if (!branchExists)
            {
                return Results.BadRequest(new[] { "Sucursal no encontrada." });
            }

            // Los precios se recalculan contra la tarifa ya congelada en la guía (CustomerPriceListId),
            // no contra la tarifa activa actual del cliente, para preservar el histórico.
            var priceList = await db.CustomerPriceLists
                .Include(x => x.Details).ThenInclude(d => d.ServiceItem)
                .FirstAsync(x => x.Id == guide.CustomerPriceListId);

            var newDetails = new List<PickupGuideDetail>();
            foreach (var detailRequest in request.Details)
            {
                var priceDetail = priceList.Details.FirstOrDefault(d => d.ServiceItemId == detailRequest.ServiceItemId);
                if (priceDetail is null)
                {
                    return Results.BadRequest(new[] { $"El servicio '{detailRequest.ServiceItemId}' no está tarifado para este cliente." });
                }

                var isPricedByWeight = priceDetail.ServiceItem.IsPeso == "S";
                if (isPricedByWeight)
                {
                    if (!detailRequest.Weight.HasValue || detailRequest.Weight.Value <= 0)
                    {
                        return Results.BadRequest(new[] { $"El servicio '{priceDetail.ServiceItem.Name}' se cobra por peso; debe indicar el peso." });
                    }
                }
                else if (detailRequest.Quantity <= 0)
                {
                    return Results.BadRequest(new[] { $"El servicio '{priceDetail.ServiceItem.Name}' se cobra por cantidad; debe indicar la cantidad." });
                }

                var basis = isPricedByWeight ? detailRequest.Weight!.Value : detailRequest.Quantity;

                newDetails.Add(new PickupGuideDetail
                {
                    ServiceItemId = detailRequest.ServiceItemId,
                    Quantity = detailRequest.Quantity,
                    Weight = detailRequest.Weight,
                    UnitPrice = priceDetail.UnitPrice,
                    Total = basis * priceDetail.UnitPrice,
                    Observation = detailRequest.Observation
                });
            }

            guide.BranchSalesId = request.BranchSalesId;
            guide.PickupDate = request.PickupDate;
            guide.Observation = request.Observation;

            db.PickupGuideDetails.RemoveRange(guide.Details);
            guide.Details = newDetails;
            guide.TotalAmount = newDetails.Sum(d => d.Total);
            guide.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            await db.SaveChangesAsync();

            await db.Entry(guide).Collection(x => x.Details).Query()
                .Include(x => x.ServiceItem)
                .LoadAsync();

            return Results.Ok(new ApiResponse<PickupGuideResponse>
            {
                Success = true,
                Message = "Guía de recojo actualizada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = PickupGuidesEndpoints.ToResponse(guide)
            });
        })
        .WithName("UpdatePickupGuide")
        .Produces<ApiResponse<PickupGuideResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
