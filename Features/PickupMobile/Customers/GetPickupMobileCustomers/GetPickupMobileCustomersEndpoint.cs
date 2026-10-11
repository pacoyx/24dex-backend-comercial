using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetPickupMobileCustomersEndpoint
{
    public static void MapGetPickupMobileCustomers(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customers", async ([FromQuery] string? search, RecepcionDbContext db) =>
        {
            var query = db.Customers
                .AsNoTracking()
                .Include(x => x.BillingProfile)
                .Where(x => x.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.FirtsName.Contains(search) ||
                    (x.LastName != null && x.LastName.Contains(search)) ||
                    (x.BillingProfile != null && x.BillingProfile.BusinessName.Contains(search)));
            }

            var customers = await query.ToListAsync();

            var response = customers
                .Select(c => new PickupMobileCustomerLookupDto(c.Id, PickupGuidesEndpoints.GetCustomerDisplayName(c)))
                .OrderBy(c => c.BusinessName)
                .ToList();

            return Results.Ok(new ApiResponse<List<PickupMobileCustomerLookupDto>>
            {
                Success = true,
                Message = "Clientes disponibles para recojo.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetPickupMobileCustomers")
        .Produces<ApiResponse<List<PickupMobileCustomerLookupDto>>>(StatusCodes.Status200OK);
    }
}
