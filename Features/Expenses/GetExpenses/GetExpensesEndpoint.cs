using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetExpensesEndpoint
{
    public static void MapGetExpenses(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] int? branchSalesId,
            [FromQuery] int? expenseCategoryId,
            [FromQuery] ExpenseSourceType? sourceType,
            RecepcionDbContext db) =>
        {
            var query = db.Expenses
                .AsNoTracking()
                .Include(x => x.BranchSales)
                .Include(x => x.ExpenseCategory)
                .Where(x => x.IsActive)
                .AsQueryable();

            if (dateFrom.HasValue)
            {
                query = query.Where(x => x.ExpenseDate.Date >= dateFrom.Value.Date);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(x => x.ExpenseDate.Date <= dateTo.Value.Date);
            }

            if (branchSalesId.HasValue)
            {
                query = query.Where(x => x.BranchSalesId == branchSalesId.Value);
            }

            if (expenseCategoryId.HasValue)
            {
                query = query.Where(x => x.ExpenseCategoryId == expenseCategoryId.Value);
            }

            if (sourceType.HasValue)
            {
                query = query.Where(x => x.SourceType == sourceType.Value);
            }

            var expenses = await query
                .OrderByDescending(x => x.ExpenseDate)
                .Select(x => new ExpenseSummaryResponse(
                    x.Id,
                    x.ExpenseDate,
                    x.BranchSalesId,
                    x.BranchSales.Description,
                    x.ExpenseCategoryId,
                    x.ExpenseCategory.Name,
                    x.SourceType,
                    x.Amount,
                    x.Description,
                    x.HasSupportingDocument,
                    x.DocumentNumber,
                    x.SupplierName
                ))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<ExpenseSummaryResponse>>
            {
                Success = true,
                Message = "Listado de gastos.",
                StatusCode = StatusCodes.Status200OK,
                Data = expenses
            });
        });
    }
}
