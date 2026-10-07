using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetPickupGuidesByCustomerReportEndpoint
{
    public static void MapGetPickupGuidesByCustomerReport(this IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/by-customer", async (
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] PickupGuideStatus? status,
            RecepcionDbContext db) =>
        {
            var query = db.PickupGuides
                .AsNoTracking()
                .Include(x => x.Customer).ThenInclude(c => c.BillingProfile)
                .Include(x => x.Details)
                .AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(x => x.PickupDate.Date >= fromDate.Value.Date);
            }

            if (toDate.HasValue)
            {
                query = query.Where(x => x.PickupDate.Date <= toDate.Value.Date);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            var guides = await query.ToListAsync();

            var report = guides
                .GroupBy(x => new { x.CustomerId, CustomerName = PickupGuidesEndpoints.GetCustomerDisplayName(x.Customer) })
                .Select(g => new PickupGuideByCustomerReportItem(
                    g.Key.CustomerId,
                    g.Key.CustomerName,
                    g.Count(),
                    g.SelectMany(x => x.Details).Sum(d => d.Weight ?? 0),
                    g.Sum(x => x.TotalAmount)
                ))
                .OrderByDescending(x => x.TotalAmount)
                .ToList();

            return Results.Ok(new ApiResponse<List<PickupGuideByCustomerReportItem>>
            {
                Success = true,
                Message = "Resumen de guías por cliente.",
                StatusCode = StatusCodes.Status200OK,
                Data = report
            });
        })
        .WithName("GetPickupGuidesByCustomerReport")
        .Produces<ApiResponse<List<PickupGuideByCustomerReportItem>>>(StatusCodes.Status200OK);
    }
}
