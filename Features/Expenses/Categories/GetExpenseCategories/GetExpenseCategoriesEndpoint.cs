using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetExpenseCategoriesEndpoint
{
    public static void MapGetExpenseCategories(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            [FromQuery] bool? isActive,
            RecepcionDbContext db) =>
        {
            var query = db.ExpenseCategories.AsNoTracking().AsQueryable();

            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive.Value);
            }

            var categories = await query
                .OrderBy(x => x.Name)
                .Select(x => new ExpenseCategoryResponse(x.Id, x.Name, x.IsActive, x.CreatedAt))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<ExpenseCategoryResponse>>
            {
                Success = true,
                Message = "Listado de categorías de gasto.",
                StatusCode = StatusCodes.Status200OK,
                Data = categories
            });
        });
    }
}
