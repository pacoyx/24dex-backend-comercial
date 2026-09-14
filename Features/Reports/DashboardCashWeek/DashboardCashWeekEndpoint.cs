using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class DashboardCashWeekEndpoint
{
    public static void MapDashboardCashWeek(this IEndpointRouteBuilder app)
    {
        app.MapGet("/dashboardcash/week", async ([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin, [FromQuery] int sucursal, RecepcionDbContext dbContext) =>
        {
            var query = dbContext.CashBoxMains
                .Include(g => g.CashBoxDetails)
                .AsNoTracking()
                .Where(g => g.FechaCaja.Date >= fechaInicio.Date && g.FechaCaja.Date <= fechaFin.Date && g.EstadoRegistro == "A");

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
