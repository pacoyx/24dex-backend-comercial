using Microsoft.EntityFrameworkCore;

public static class ClonePriceListEndpoint
{
    public static void MapClonePriceList(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:long}/clone", async (long id, ClonePriceListRequest request, RecepcionDbContext db) =>
        {
            var source = await db.CustomerPriceLists
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (source is null)
            {
                return Results.NotFound("Tarifa no encontrada.");
            }

            if (request.EffectiveTo.HasValue && request.EffectiveFrom.HasValue && request.EffectiveTo < request.EffectiveFrom)
            {
                return Results.BadRequest(new[] { "La fecha de fin de vigencia no puede ser anterior a la fecha de inicio." });
            }

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            var nextVersion = await db.CustomerPriceLists
                .Where(x => x.CustomerId == source.CustomerId)
                .MaxAsync(x => (int?)x.Version) ?? 0;
            nextVersion++;

            var clone = new CustomerPriceList
            {
                CustomerId = source.CustomerId,
                Name = request.Name ?? source.Name,
                Version = nextVersion,
                EffectiveFrom = request.EffectiveFrom ?? fechaOperacion,
                EffectiveTo = request.EffectiveTo,
                IsActive = false,
                CreatedAt = fechaOperacion,
                Details = source.Details.Select(d => new CustomerPriceListDetail
                {
                    ServiceItemId = d.ServiceItemId,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };

            await db.CustomerPriceLists.AddAsync(clone);
            await db.SaveChangesAsync();

            await db.Entry(clone).Collection(x => x.Details).Query()
                .Include(x => x.ServiceItem)
                .LoadAsync();

            var response = new ApiResponse<CustomerPriceListResponse>
            {
                Success = true,
                Message = "Tarifa clonada correctamente. Modifique los precios necesarios y actívela cuando esté lista.",
                StatusCode = StatusCodes.Status201Created,
                Data = CustomerPriceListsEndpoints.ToResponse(clone)
            };

            return Results.Created($"/api/price-lists/{clone.Id}", response);
        })
        .WithName("CloneCustomerPriceList")
        .Produces<ApiResponse<CustomerPriceListResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
