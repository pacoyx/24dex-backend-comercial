using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetByServiceEndpoint
{
    public static void MapGetByService(this IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/by-service", async (
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            ClaimsPrincipal principal,
            RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var query = db.PickupGuideDetails
                .AsNoTracking()
                .Include(x => x.ServiceItem)
                .Include(x => x.PickupGuide)
                .Where(x => x.PickupGuide.CustomerId == customerId.Value)
                .AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(x => x.PickupGuide.PickupDate.Date >= fromDate.Value.Date);
            }

            if (toDate.HasValue)
            {
                query = query.Where(x => x.PickupGuide.PickupDate.Date <= toDate.Value.Date);
            }

            var details = await query.ToListAsync();

            var report = details
                .GroupBy(x => new { x.ServiceItemId, x.ServiceItem.Name })
                .Select(g => new PortalServiceConsumptionItem(
                    g.Key.ServiceItemId,
                    g.Key.Name,
                    g.Select(x => x.PickupGuideId).Distinct().Count(),
                    g.Sum(x => x.Quantity),
                    g.Sum(x => x.Weight ?? 0),
                    g.Sum(x => x.Total)
                ))
                .OrderByDescending(x => x.TotalAmount)
                .ToList();

            return Results.Ok(new ApiResponse<List<PortalServiceConsumptionItem>>
            {
                Success = true,
                Message = "Consumo por servicio obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = report
            });
        })
        .WithName("GetPortalServiceConsumption")
        .Produces<ApiResponse<List<PortalServiceConsumptionItem>>>(StatusCodes.Status200OK)
        .RequireCustomerPermission(CustomerPermissionCodes.ReportsView);
    }
}
