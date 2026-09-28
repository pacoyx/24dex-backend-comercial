public static class ExpenseCategoriesEndpoints
{
    public static void MapExpenseCategories(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/expense-categories").WithTags("ExpenseCategories").RequirePermission(PermissionCodes.Expenses);

        group.MapGetExpenseCategories();
        group.MapCreateExpenseCategory();
        group.MapUpdateExpenseCategory();
    }
}
