using Microsoft.EntityFrameworkCore;

public static class GetExpensesByCategoryEndpoint
{
    public static void MapGetExpensesByCategory(this IEndpointRouteBuilder app)
    {
        app.MapGet("/by-category", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var response = await db.Expenses
                .AsNoTracking()
                .Where(x => x.IsActive && x.ExpenseDate >= monthStart && x.ExpenseDate < monthEnd)
                .GroupBy(x => x.ExpenseCategory.Name)
                .Select(g => new ExpenseByCategoryResponse(g.Key, g.Sum(x => x.Amount)))
                .OrderByDescending(x => x.Amount)
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<ExpenseByCategoryResponse>>
            {
                Success = true,
                Message = "Gastos por categoría del mes.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
