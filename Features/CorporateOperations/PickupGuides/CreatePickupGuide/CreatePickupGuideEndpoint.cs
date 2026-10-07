using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreatePickupGuideEndpoint
{
    public static void MapCreatePickupGuide(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreatePickupGuideRequest request,
            IValidator<CreatePickupGuideRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var customer = await db.Customers
                .Include(x => x.BillingProfile)
                .FirstOrDefaultAsync(x => x.Id == request.CustomerId);
            if (customer is null)
            {
                return Results.BadRequest(new[] { "Cliente no encontrado." });
            }

            var branchExists = await db.BranchSales.AnyAsync(x => x.Id == request.BranchSalesId);
            if (!branchExists)
            {
                return Results.BadRequest(new[] { "Sucursal no encontrada." });
            }

            var activePriceList = await db.CustomerPriceLists
                .Include(x => x.Details).ThenInclude(d => d.ServiceItem)
                .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId && x.IsActive);

            if (activePriceList is null)
            {
                return Results.BadRequest(new[] { "El cliente no tiene una tarifa activa. No se puede registrar la guía." });
            }

            var details = new List<PickupGuideDetail>();
            foreach (var detailRequest in request.Details)
            {
                var priceDetail = activePriceList.Details.FirstOrDefault(d => d.ServiceItemId == detailRequest.ServiceItemId);
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

                details.Add(new PickupGuideDetail
                {
                    ServiceItemId = detailRequest.ServiceItemId,
                    Quantity = detailRequest.Quantity,
                    Weight = detailRequest.Weight,
                    UnitPrice = priceDetail.UnitPrice,
                    Total = basis * priceDetail.UnitPrice,
                    Observation = detailRequest.Observation
                });
            }

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            var guide = new PickupGuide
            {
                GuideNumber = Guid.NewGuid().ToString("N"),
                CustomerId = request.CustomerId,
                BranchSalesId = request.BranchSalesId,
                PickupDate = request.PickupDate,
                Status = PickupGuideStatus.Draft,
                Observation = request.Observation,
                CustomerPriceListId = activePriceList.Id,
                TotalAmount = details.Sum(d => d.Total),
                CreatedAt = fechaOperacion,
                Details = details
            };

            await db.PickupGuides.AddAsync(guide);
            await db.SaveChangesAsync();

            // La guía usa su propio Id auto-generado como correlativo, garantizando
            // numeración unica e independiente de WorkGuide/Invoice/Receipt.
            guide.GuideNumber = $"GR-{guide.Id:D6}";
            await db.SaveChangesAsync();

            await db.Entry(guide).Collection(x => x.Details).Query()
                .Include(x => x.ServiceItem)
                .LoadAsync();
            guide.Customer = customer;

            var response = new ApiResponse<PickupGuideResponse>
            {
                Success = true,
                Message = "Guía de recojo registrada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = PickupGuidesEndpoints.ToResponse(guide)
            };

            return Results.Created($"/api/pickup-guides/{guide.Id}", response);
        })
        .WithName("CreatePickupGuide")
        .Produces<ApiResponse<PickupGuideResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
