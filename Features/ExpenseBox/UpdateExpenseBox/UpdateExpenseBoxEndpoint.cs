public static class UpdateExpenseBoxEndpoint{
    public static void MapUpdateExpenseBox(this IEndpointRouteBuilder app){
        app.MapPut("/{id}", async (RecepcionDbContext context, int id, UpdateExpenseBoxDto request) =>{
            var expenseBox = await context.ExpenseBoxMains.FindAsync(Convert.ToInt32(id));
            if (expenseBox == null){
                return Results.NotFound();
            }
            expenseBox.CategoryGasto = request.CategoryGasto;            
            expenseBox.PersonalAutoriza = request.PersonalAutoriza;
            expenseBox.FechaGasto = request.FechaGasto;
            expenseBox.Importe = request.Importe;
            expenseBox.DetallesEgreso = request.DetallesEgreso;
            expenseBox.EstadoRegistro = request.EstadoRegistro;
            expenseBox.UserId = request.UserId;
            expenseBox.CashBoxMainId = request.CashBoxMainId;
            
            await context.SaveChangesAsync();
            return Results.Ok(expenseBox);
        });
    }
}