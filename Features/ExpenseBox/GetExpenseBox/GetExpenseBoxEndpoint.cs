using Microsoft.EntityFrameworkCore;

public static class GetExpenseBoxEndpoints
{
    public static void MapGetExpenseBox(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext context) =>
        {
            var expenseBox = await context.ExpenseBoxMains.FindAsync(Convert.ToInt32(id));
            if (expenseBox == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(expenseBox);
        });        
    }
}