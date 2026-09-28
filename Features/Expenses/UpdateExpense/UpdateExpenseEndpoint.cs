using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class UpdateExpenseEndpoint
{
    public static void MapUpdateExpense(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:long}", async (
            long id,
            UpdateExpenseRequest request,
            IValidator<UpdateExpenseRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var expense = await db.Expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (expense is null)
            {
                return Results.NotFound("Gasto no encontrado.");
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

            expense.BranchSalesId = request.BranchSalesId;
            expense.ExpenseCategoryId = request.ExpenseCategoryId;
            expense.SourceType = request.SourceType;
            expense.ExpenseDate = request.ExpenseDate;
            expense.Amount = request.Amount;
            expense.Description = request.Description;
            expense.HasSupportingDocument = request.HasSupportingDocument;
            expense.DocumentNumber = request.DocumentNumber;
            expense.SupplierName = request.SupplierName;
            expense.Observation = request.Observation;

            await db.SaveChangesAsync();

            var branchName = await db.BranchSales
                .Where(x => x.Id == expense.BranchSalesId)
                .Select(x => x.Description)
                .FirstAsync();

            var response = new ApiResponse<ExpenseResponse>
            {
                Success = true,
                Message = "Gasto actualizado correctamente.",
                StatusCode = StatusCodes.Status200OK,
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

            return Results.Ok(response);
        });
    }
}
