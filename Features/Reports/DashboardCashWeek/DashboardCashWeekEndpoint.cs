using System.Globalization;
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

            var result = await query
                .GroupBy(g => g.FechaCaja.Date)
                .Select(group => new
                {
                    Fecha = group.Key,
                    Detalles = group
                        .SelectMany(g => g.CashBoxDetails!)
                        .GroupBy(detail => detail.TipoPago)
                        .Select(detailGroup => new
                        {
                            TipoPago = detailGroup.Key,
                            MontoTotal = detailGroup.Sum(detail => detail.Adelanto + detail.Importe)
                        })
                        .ToList()
                })
                .ToListAsync();

            var cultura = new CultureInfo("es-ES");
            var data = new List<DashboardCashWeekResponseDto>();
            for (var fecha = fechaInicio.Date; fecha <= fechaFin.Date; fecha = fecha.AddDays(1))
            {
                var dia = result.FirstOrDefault(r => r.Fecha == fecha);
                var detalles = dia?.Detalles
                    .Select(d => new DashboardCashDetailDto(d.TipoPago, d.MontoTotal))
                    .ToList() ?? new List<DashboardCashDetailDto>();

                data.Add(new DashboardCashWeekResponseDto(
                    fecha,
                    cultura.TextInfo.ToTitleCase(fecha.ToString("dddd", cultura)),
                    detalles.Sum(d => d.MontoTotal),
                    detalles
                ));
            }

            var response = new ApiResponse<List<DashboardCashWeekResponseDto>>
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
