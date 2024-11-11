public static class DeleteExpenseBoxEndpoint
{
    public static void MapDeleteExpenseBox(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (int id, RecepcionDbContext context) =>
        {
            var expenseBox = await context.ExpenseBoxMains.FindAsync(id);
            if (expenseBox == null)
            {
                 var responseErr = new ApiResponse<string>
                {
                    Data = "Gasto no encontrado",
                    Message = "Gasto no encontrado",
                    Success = false,
                    Errors = [],
                    StatusCode = 404
                };
                return Results.NotFound(responseErr);
            }
            context.ExpenseBoxMains.Remove(expenseBox);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Gasto eliminado",
                Message = "Gasto eliminado",
                Success = true,
                Errors = [],
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}