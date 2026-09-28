using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateExpenseCategoryEndpoint
{
    public static void MapCreateExpenseCategory(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateExpenseCategoryRequest request,
            IValidator<CreateExpenseCategoryRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var nameInUse = await db.ExpenseCategories.AnyAsync(x => x.Name == request.Name);
            if (nameInUse)
            {
                return Results.BadRequest("Ya existe una categoría de gasto con ese nombre.");
            }

            var category = new ExpenseCategory
            {
                Name = request.Name,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await db.ExpenseCategories.AddAsync(category);
            await db.SaveChangesAsync();

            var response = new ApiResponse<ExpenseCategoryResponse>
            {
                Success = true,
                Message = "Categoría de gasto creada correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = new ExpenseCategoryResponse(category.Id, category.Name, category.IsActive, category.CreatedAt)
            };

            return Results.Created($"/api/expense-categories/{category.Id}", response);
        });
    }
}
