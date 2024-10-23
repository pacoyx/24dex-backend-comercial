using Microsoft.EntityFrameworkCore;

public static class GetExpensesBoxEndpoint
{
    public static void MapGetExpensesBox(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var expenseBox = await context.ExpenseBoxMains.AsNoTracking().ToListAsync();
            return Results.Ok(expenseBox);
        });
    }
}