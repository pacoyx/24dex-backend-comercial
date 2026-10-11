using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public static class GetPickupMobileDashboardEndpoint
{
    public static void MapGetPickupMobileDashboard(this IEndpointRouteBuilder app)
    {
        app.MapGet("/dashboard", async (ClaimsPrincipal principal, RecepcionDbContext db) =>
        {
            var employeeId = principal.GetEmployeeId();
            if (employeeId is null)
            {
                return Results.BadRequest(new[] { "El usuario no está vinculado a un empleado. Contacte al administrador para habilitar el acceso móvil." });
            }

            var today = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")).Date;

            var baseQuery = db.PickupGuides.AsNoTracking().Where(x => x.EmployeeId == employeeId.Value);

            var todayCount = await baseQuery.CountAsync(x => x.PickupDate.Date == today);
            var draftCount = await baseQuery.CountAsync(x => x.Status == PickupGuideStatus.Draft);

            return Results.Ok(new ApiResponse<PickupMobileDashboardResponse>
            {
                Success = true,
                Message = "Resumen del día.",
                StatusCode = StatusCodes.Status200OK,
                Data = new PickupMobileDashboardResponse(todayCount, draftCount)
            });
        })
        .WithName("GetPickupMobileDashboard")
        .Produces<ApiResponse<PickupMobileDashboardResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
