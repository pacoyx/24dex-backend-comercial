using Microsoft.AspNetCore.Routing;

public static class ExpenseBoxEndpoints
{
    public static void MapExpenseBox(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/expenseBox").WithTags("ExpenseBox");
        group.MapCreateExpenseBox();
        group.MapGetExpenseBox();
        group.MapGetExpensesBox();
        group.MapDeleteExpenseBox();
        group.MapUpdateExpenseBox();        
    }
}