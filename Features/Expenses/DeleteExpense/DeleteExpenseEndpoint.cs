using Microsoft.EntityFrameworkCore;

public static class DeleteExpenseEndpoint
{
    public static void MapDeleteExpense(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:long}", async (long id, RecepcionDbContext db) =>
        {
            var expense = await db.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (expense is null)
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Gasto no encontrado.",
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = "Gasto no encontrado."
                });
            }

            expense.IsActive = false;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Gasto eliminado.",
                StatusCode = StatusCodes.Status200OK,
                Data = "Gasto eliminado."
            });
        });
    }
}
