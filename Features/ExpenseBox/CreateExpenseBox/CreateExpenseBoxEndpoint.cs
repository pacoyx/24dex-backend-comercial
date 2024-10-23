public static class CreateExpenseBoxEndpoint
{
    public static void MapCreateExpenseBox(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (CreateExpenseBoxDto dto, RecepcionDbContext context) =>
        {
            var expenseBox = new ExpenseBox
            {
                CategoryGasto = dto.CategoryGasto,
                PersonalAutoriza = dto.PersonalAutoriza,
                FechaGasto = dto.FechaGasto,
                Importe = dto.Importe,
                DetallesEgreso = dto.DetallesEgreso,
                EstadoRegistro = dto.EstadoRegistro
            };
            await context.ExpenseBoxMains.AddAsync(expenseBox);
            await context.SaveChangesAsync();
            return Results.CreatedAtRoute($"/api/expenseBoxes/{expenseBox.Id}", expenseBox);
        });
    }
}