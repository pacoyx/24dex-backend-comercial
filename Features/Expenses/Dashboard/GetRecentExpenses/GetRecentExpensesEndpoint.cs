using Microsoft.EntityFrameworkCore;

public static class GetRecentExpensesEndpoint
{
    public static void MapGetRecentExpenses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/recent-expenses", async (RecepcionDbContext db) =>
        {
            var response = await db.Expenses
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.ExpenseDate)
                .Take(20)
                .Select(x => new RecentExpenseResponse(
                    x.Id, x.ExpenseDate, x.BranchSales.Description, x.ExpenseCategory.Name, x.Amount, x.Description))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<RecentExpenseResponse>>
            {
                Success = true,
                Message = "Últimos gastos registrados.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
