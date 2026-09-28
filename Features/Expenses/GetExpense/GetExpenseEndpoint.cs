using Microsoft.EntityFrameworkCore;

public static class GetExpenseEndpoint
{
    public static void MapGetExpense(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:long}", async (long id, RecepcionDbContext db) =>
        {
            var expense = await db.Expenses
                .AsNoTracking()
                .Include(x => x.BranchSales)
                .Include(x => x.ExpenseCategory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (expense is null)
            {
                return Results.NotFound("Gasto no encontrado.");
            }

            var response = new ExpenseResponse(
                expense.Id,
                expense.BranchSalesId,
                expense.BranchSales.Description,
                expense.ExpenseCategoryId,
                expense.ExpenseCategory.Name,
                expense.SourceType,
                expense.ExpenseDate,
                expense.Amount,
                expense.Description,
                expense.HasSupportingDocument,
                expense.DocumentNumber,
                expense.SupplierName,
                expense.Observation,
                expense.IsActive,
                expense.CreatedAt);

            return Results.Ok(new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Gasto encontrado.",
                StatusCode = StatusCodes.Status200OK,
                Data = response
            });
        });
    }
}
