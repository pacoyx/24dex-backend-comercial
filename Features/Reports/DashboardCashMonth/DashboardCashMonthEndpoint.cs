using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class DashboardCashMonthEndpoint
{
    public static void MapDashboardCashMonth(this IEndpointRouteBuilder app)
    {
        app.MapGet("/dashboardcash/month", async ([FromQuery] int mes, [FromQuery] int anio, [FromQuery] int sucursal, RecepcionDbContext dbContext) =>
        {
            var query = dbContext.CashBoxMains
                .Include(g => g.CashBoxDetails)
                .AsNoTracking()
                .Where(g => g.FechaCaja.Month == mes && g.FechaCaja.Year == anio && g.EstadoRegistro == "A");

            if (sucursal > 0)
            {
                query = query.Where(g => g.BranchSalesId == sucursal);
            }

            var data = await DashboardCashQueryHelper.BuildResponseAsync(query, dbContext);

            var response = new ApiResponse<List<DashboardCashResponseDto>>
            {
                Data = data,
                Success = true,
                Message = "",
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}
