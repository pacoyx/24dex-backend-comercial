using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetMonthlySummaryEndpoint
{
    public static void MapGetMonthlySummary(this IEndpointRouteBuilder app)
    {
        app.MapGet("/reports/monthly-summary", async (
            [FromQuery] int? year,
            [FromQuery] int? month,
            ClaimsPrincipal principal,
            RecepcionDbContext db) =>
        {
            var customerId = principal.GetCustomerId();
            if (customerId is null)
            {
                return Results.Unauthorized();
            }

            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            var targetYear = year ?? now.Year;
            var targetMonth = month ?? now.Month;

            var guides = await db.PickupGuides
                .AsNoTracking()
                .Include(x => x.Details)
                .Where(x => x.CustomerId == customerId.Value
                    && x.PickupDate.Year == targetYear
                    && x.PickupDate.Month == targetMonth)
                .ToListAsync();

            var response = new MonthlySummaryResponse(
                guides.Count,
                guides.SelectMany(x => x.Details).Sum(d => d.Weight ?? 0),
                guides.Sum(x => x.TotalAmount)
            );

            return Results.Ok(new ApiResponse<MonthlySummaryResponse>
            {
                Success = true,
                Message = "Resumen mensual obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        })
        .WithName("GetPortalMonthlySummary")
        .Produces<ApiResponse<MonthlySummaryResponse>>(StatusCodes.Status200OK)
        .RequireCustomerPermission(CustomerPermissionCodes.ReportsView);
    }
}
