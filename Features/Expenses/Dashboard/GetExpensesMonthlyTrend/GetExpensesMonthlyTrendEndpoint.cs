using Microsoft.EntityFrameworkCore;

public static class GetExpensesMonthlyTrendEndpoint
{
    private static readonly string[] MonthAbbreviations =
    {
        "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"
    };

    public static void MapGetExpensesMonthlyTrend(this IEndpointRouteBuilder app)
    {
        app.MapGet("/monthly-trend", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var rangeStart = new DateTime(today.Year, today.Month, 1).AddMonths(-5);

            var administrativeByMonth = await db.Expenses
                .AsNoTracking()
                .Where(x => x.IsActive && x.ExpenseDate >= rangeStart)
                .GroupBy(x => new { x.ExpenseDate.Year, x.ExpenseDate.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Amount = g.Sum(x => x.Amount) })
                .ToListAsync();

            var operationalByMonth = await db.ExpenseBoxMains
                .AsNoTracking()
                .Where(x => x.FechaGasto >= rangeStart)
                .GroupBy(x => new { x.FechaGasto.Year, x.FechaGasto.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Amount = g.Sum(x => x.Importe) })
                .ToListAsync();

            var response = new List<ExpenseMonthlyTrendResponse>();
            for (var i = 5; i >= 0; i--)
            {
                var month = rangeStart.AddMonths(5 - i);
                var administrative = administrativeByMonth
                    .Where(x => x.Year == month.Year && x.Month == month.Month)
                    .Sum(x => x.Amount);
                var operational = operationalByMonth
                    .Where(x => x.Year == month.Year && x.Month == month.Month)
                    .Sum(x => x.Amount);

                response.Add(new ExpenseMonthlyTrendResponse(MonthAbbreviations[month.Month - 1], administrative + operational));
            }

            return Results.Ok(new ApiResponse<List<ExpenseMonthlyTrendResponse>>
            {
                Success = true,
                Message = "Tendencia de gastos de los últimos 6 meses.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
