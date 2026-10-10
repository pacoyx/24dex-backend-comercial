using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetHistoryEndpoint
{
    public static void MapGetHistory(this IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/history", async (
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

            var query = db.PickupGuides
                .AsNoTracking()
                .Include(x => x.Details)
                .Where(x => x.CustomerId == customerId.Value)
                .AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(x => x.PickupDate.Date >= fromDate.Value.Date);
            }

            if (toDate.HasValue)
            {
                query = query.Where(x => x.PickupDate.Date <= toDate.Value.Date);
            }

            var guides = await query.ToListAsync();

            var history = guides
                .GroupBy(x => new { x.PickupDate.Year, x.PickupDate.Month })
                .Select(g => new PortalHistoryItem(
                    g.Key.Year,
                    g.Key.Month,
                    g.Count(),
                    g.SelectMany(x => x.Details).Sum(d => d.Weight ?? 0),
                    g.Sum(x => x.TotalAmount)
                ))
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            return Results.Ok(new ApiResponse<List<PortalHistoryItem>>
            {
                Success = true,
                Message = "Histórico obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = history
            });
        })
        .WithName("GetPortalHistory")
        .Produces<ApiResponse<List<PortalHistoryItem>>>(StatusCodes.Status200OK)
        .RequireCustomerPermission(CustomerPermissionCodes.ReportsView);
    }
}
