using Microsoft.EntityFrameworkCore;

public static class DashboardCashQueryHelper
{
    public static async Task<List<DashboardCashResponseDto>> BuildResponseAsync(IQueryable<CashBoxMain> query, RecepcionDbContext dbContext)
    {
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

        return result.Select(r => new DashboardCashResponseDto
        (
            r.BranchSalesId,
            r.Descripcion!,
            r.Detalles.Select(d => new DashboardCashDetailDto
            (
                 d.TipoPago,
                 d.MontoTotal
            )).ToList()
        )).ToList();
    }
}
