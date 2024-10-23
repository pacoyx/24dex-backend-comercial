public static class DeleteExpenseBoxEndpoint
{
    public static void MapDeleteExpenseBox(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (int id, RecepcionDbContext context) =>
        {
            var expenseBox = await context.ExpenseBoxMains.FindAsync(Convert.ToInt32(id));
            if (expenseBox == null)
            {
                return Results.NotFound();
            }
            context.ExpenseBoxMains.Remove(expenseBox);
            await context.SaveChangesAsync();
            return Results.Ok(expenseBox);
        });
    }
}