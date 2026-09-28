using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateExpenseEndpoint
{
    public static void MapCreateExpense(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateExpenseRequest request,
            IValidator<CreateExpenseRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var branchExists = await db.BranchSales.AnyAsync(x => x.Id == request.BranchSalesId);
            if (!branchExists)
            {
                return Results.NotFound("Sucursal no encontrada.");
            }

            var category = await db.ExpenseCategories
                .Where(x => x.Id == request.ExpenseCategoryId && x.IsActive)
                .Select(x => x.Name)
                .FirstOrDefaultAsync();
            if (category is null)
            {
                return Results.NotFound("Categoría de gasto no encontrada.");
            }

            var expense = new Expense
            {
                BranchSalesId = request.BranchSalesId,
                ExpenseCategoryId = request.ExpenseCategoryId,
                SourceType = request.SourceType,
                ExpenseDate = request.ExpenseDate,
                Amount = request.Amount,
                Description = request.Description,
                HasSupportingDocument = request.HasSupportingDocument,
                DocumentNumber = request.DocumentNumber,
                SupplierName = request.SupplierName,
                Observation = request.Observation,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await db.Expenses.AddAsync(expense);
            await db.SaveChangesAsync();

            var branchName = await db.BranchSales
                .Where(x => x.Id == expense.BranchSalesId)
                .Select(x => x.Description)
                .FirstAsync();

            var response = new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Gasto registrado correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = new ExpenseResponse(
                    expense.Id,
                    expense.BranchSalesId,
                    branchName,
                    expense.ExpenseCategoryId,
                    category,
                    expense.SourceType,
                    expense.ExpenseDate,
                    expense.Amount,
                    expense.Description,
                    expense.HasSupportingDocument,
                    expense.DocumentNumber,
                    expense.SupplierName,
                    expense.Observation,
                    expense.IsActive,
                    expense.CreatedAt)
            };

            return Results.Created($"/api/expenses/{expense.Id}", response);
        });
    }
}
