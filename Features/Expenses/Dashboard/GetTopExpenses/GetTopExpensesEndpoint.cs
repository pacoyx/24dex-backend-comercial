using Microsoft.EntityFrameworkCore;

public static class GetTopExpensesEndpoint
{
    public static void MapGetTopExpenses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/top-expenses", async (RecepcionDbContext db) =>
        {
            var today = DateTime.UtcNow.Date;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var response = await db.Expenses
                .AsNoTracking()
                .Where(x => x.IsActive && x.ExpenseDate >= monthStart && x.ExpenseDate < monthEnd)
                .OrderByDescending(x => x.Amount)
                .Take(20)
                .Select(x => new TopExpenseResponse(
                    x.Id, x.ExpenseDate, x.BranchSales.Description, x.ExpenseCategory.Name, x.Amount, x.Description))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<TopExpenseResponse>>
            {
                Success = true,
                Message = "Mayores gastos del mes.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
