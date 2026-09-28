using Microsoft.EntityFrameworkCore;

public static class GetExpensesByBranchEndpoint
{
    public static void MapGetExpensesByBranch(this IEndpointRouteBuilder app)
    {
        app.MapGet("/by-branch", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var totals = await db.Expenses
                .AsNoTracking()
                .Where(x => x.IsActive && x.ExpenseDate >= monthStart && x.ExpenseDate < monthEnd)
                .GroupBy(x => x.BranchSales.Description)
                .Select(g => new { Branch = g.Key, Amount = g.Sum(x => x.Amount) })
                .OrderByDescending(x => x.Amount)
                .ToListAsync();

            var response = totals
                .Select(x => new ExpenseByBranchResponse(x.Branch, x.Amount))
                .ToList();

            return Results.Ok(new ApiResponse<List<ExpenseByBranchResponse>>
            {
                Success = true,
                Message = "Gastos por sucursal del mes.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
