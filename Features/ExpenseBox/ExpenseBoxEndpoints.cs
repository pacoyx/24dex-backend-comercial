public static class ExpenseBoxEndpoints
{
    public static void MapExpenseBox(this WebApplication app)
    {
        var group = app.MapGroup("/api/expenseBox");
        group.MapCreateExpenseBox();
        group.MapGetExpenseBox();
        group.MapGetExpensesBox();
        group.MapDeleteExpenseBox();
        group.MapUpdateExpenseBox();
    }
}