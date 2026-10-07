using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetPickupGuidesMonthlyReportEndpoint
{
    public static void MapGetPickupGuidesMonthlyReport(this IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/monthly", async (
            [FromQuery] int? year,
            [FromQuery] int? customerId,
            [FromQuery] PickupGuideStatus? status,
            RecepcionDbContext db) =>
        {
            var query = db.PickupGuides
                .AsNoTracking()
                .Include(x => x.Details)
                .AsQueryable();

            if (year.HasValue)
            {
                query = query.Where(x => x.PickupDate.Year == year.Value);
            }

            if (customerId.HasValue)
            {
                query = query.Where(x => x.CustomerId == customerId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            var guides = await query.ToListAsync();

            var report = guides
                .GroupBy(x => new { x.PickupDate.Year, x.PickupDate.Month })
                .Select(g => new PickupGuideMonthlyReportItem(
                    g.Key.Year,
                    g.Key.Month,
                    g.Count(),
                    g.SelectMany(x => x.Details).Sum(d => d.Weight ?? 0),
                    g.Sum(x => x.TotalAmount)
                ))
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            return Results.Ok(new ApiResponse<List<PickupGuideMonthlyReportItem>>
            {
                Success = true,
                Message = "Resumen mensual de guías.",
                StatusCode = StatusCodes.Status200OK,
                Data = report
            });
        })
        .WithName("GetPickupGuidesMonthlyReport")
        .Produces<ApiResponse<List<PickupGuideMonthlyReportItem>>>(StatusCodes.Status200OK);
    }
}
