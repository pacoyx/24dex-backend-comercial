using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class UpdateExpenseCategoryEndpoint
{
    public static void MapUpdateExpenseCategory(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", async (
            int id,
            UpdateExpenseCategoryRequest request,
            IValidator<UpdateExpenseCategoryRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var category = await db.ExpenseCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                return Results.NotFound("Categoría de gasto no encontrada.");
            }

            var nameInUse = await db.ExpenseCategories.AnyAsync(x => x.Name == request.Name && x.Id != id);
            if (nameInUse)
            {
                return Results.BadRequest("Ya existe una categoría de gasto con ese nombre.");
            }

            category.Name = request.Name;
            category.IsActive = request.IsActive;

            await db.SaveChangesAsync();

            var response = new ApiResponse<ExpenseCategoryResponse>
            {
                Success = true,
                Message = "Categoría de gasto actualizada correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new ExpenseCategoryResponse(category.Id, category.Name, category.IsActive, category.CreatedAt)
            };

            return Results.Ok(response);
        });
    }
}
