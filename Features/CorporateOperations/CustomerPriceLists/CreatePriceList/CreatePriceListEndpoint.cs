using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreatePriceListEndpoint
{
    public static void MapCreatePriceList(this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:int}/price-lists", async (
            int id,
            CreatePriceListRequest request,
            IValidator<CreatePriceListRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var customerExists = await db.Customers.AnyAsync(x => x.Id == id);
            if (!customerExists)
            {
                return Results.NotFound("Cliente no encontrado.");
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

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            var nextVersion = await db.CustomerPriceLists
                .Where(x => x.CustomerId == id)
                .Select(x => (int?)x.Version)
                .MaxAsync() ?? 0;
            nextVersion++;

            if (request.Activate)
            {
                var currentlyActive = await db.CustomerPriceLists
                    .Where(x => x.CustomerId == id && x.IsActive)
                    .ToListAsync();
                foreach (var active in currentlyActive)
                {
                    active.IsActive = false;
                }
            }

            var priceList = new CustomerPriceList
            {
                CustomerId = id,
                Name = request.Name,
                Version = nextVersion,
                EffectiveFrom = request.EffectiveFrom,
                EffectiveTo = request.EffectiveTo,
                IsActive = request.Activate,
                CreatedAt = fechaOperacion,
                Details = request.Details.Select(d => new CustomerPriceListDetail
                {
                    ServiceItemId = d.ServiceItemId,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };

            await db.CustomerPriceLists.AddAsync(priceList);
            await db.SaveChangesAsync();

            await db.Entry(priceList).Collection(x => x.Details).Query()
                .Include(x => x.ServiceItem)
                .LoadAsync();

            var response = new ApiResponse<CustomerPriceListResponse>
            {
                Success = true,
                Message = "Tarifa registrada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = CustomerPriceListsEndpoints.ToResponse(priceList)
            };

            return Results.Created($"/api/price-lists/{priceList.Id}", response);
        })
        .WithName("CreateCustomerPriceList")
        .Produces<ApiResponse<CustomerPriceListResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
