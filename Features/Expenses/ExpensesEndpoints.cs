public static class ExpensesEndpoints
{
    public static void MapExpenses(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/expenses").WithTags("Expenses");

        group.MapCreateExpense();
        group.MapUpdateExpense();
        group.MapGetExpense();
        group.MapGetExpenses();
        group.MapDeleteExpense();

        group.MapCreateExpenseAttachment();
        group.MapGetExpenseAttachments();
    }
}
