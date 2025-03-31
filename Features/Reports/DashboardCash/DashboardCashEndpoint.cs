using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class DashboardCashEndpoint
{
    public static void MapDashboardCash(this IEndpointRouteBuilder app)
    {
        app.MapGet("/dashboardcash", async ([FromQuery] DateTime date, [FromQuery] int sucursal, RecepcionDbContext dbContext) =>
        {
            var query = dbContext.CashBoxMains
                .Include(g => g.CashBoxDetails)
                .AsNoTracking()
                .Where(g => g.FechaCaja.Date == date.Date && g.EstadoRegistro == "A");

            if (sucursal > 0)
            {
                query = query.Where(g => g.BranchSalesId == sucursal);
            }

            var result = await query
                .GroupBy(g => g.BranchSalesId)
                .Select(group => new
                {
                    BranchSalesId = group.Key,
                    Descripcion = dbContext.BranchSales
                        .Where(branch => branch.Id == group.Key)
                        .Select(branch => branch.Description)
                        .FirstOrDefault(),
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


            var responsePrevio = result.Select(r => new DashboardCashResponseDto
            (
                r.BranchSalesId,
                r.Descripcion!,
                r.Detalles.Select(d => new DashboardCashDetailDto
                (
                     d.TipoPago,
                     d.MontoTotal
                )).ToList()
            )).ToList();


            var response = new ApiResponse<List<DashboardCashResponseDto>>
            {
                Data = responsePrevio,
                Success = true,
                Message = "",
                StatusCode = 200
            };

            return Results.Ok(response);

        });
    }

}