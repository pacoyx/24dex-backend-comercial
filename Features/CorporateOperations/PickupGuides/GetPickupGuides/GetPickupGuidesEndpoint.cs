using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetPickupGuidesEndpoint
{
    public static void MapGetPickupGuides(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            [FromQuery] int? customerId,
            [FromQuery] PickupGuideStatus? status,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            RecepcionDbContext db) =>
        {
            var query = db.PickupGuides
                .AsNoTracking()
                .Include(x => x.Customer).ThenInclude(c => c.BillingProfile)
                .AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(x => x.CustomerId == customerId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(x => x.PickupDate.Date >= fromDate.Value.Date);
            }

            if (toDate.HasValue)
            {
                query = query.Where(x => x.PickupDate.Date <= toDate.Value.Date);
            }

            var guides = await query.OrderByDescending(x => x.PickupDate).ToListAsync();

            return Results.Ok(new ApiResponse<List<PickupGuideSummaryResponse>>
            {
                Success = true,
                Message = "Listado de guías de recojo.",
                StatusCode = StatusCodes.Status200OK,
                Data = guides.Select(PickupGuidesEndpoints.ToSummaryResponse).ToList()
            });
        })
        .WithName("GetPickupGuides")
        .Produces<ApiResponse<List<PickupGuideSummaryResponse>>>(StatusCodes.Status200OK);
    }
}
