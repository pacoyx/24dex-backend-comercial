public static class UpdateExpenseBoxEndpoint
{
    public static void MapUpdateExpenseBox(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (RecepcionDbContext context, int id, UpdateExpenseBoxDto request, IAppLogger<string> logger) =>
        {
            var expenseBox = await context.ExpenseBoxMains.FindAsync(id);
            if (expenseBox == null)
            {
                logger.LogWarning("Gasto no encontrado", "UpdateExpenseBoxEndpoint");
                var responseErr = new ApiResponse<string>
                {
                    Data = "Gasto no encontrado",
                    Message = "Gasto no encontrado",
                    Success = false,
                    Errors = [],
                    StatusCode = 404
                };
                return Results.NotFound(responseErr);
            }

            expenseBox.CategoryGasto = request.CategoryGasto;
            expenseBox.PersonalAutoriza = request.PersonalAutoriza;
            expenseBox.Importe = request.Importe;
            expenseBox.DetallesEgreso = request.DetallesEgreso;

            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Gasto actualizado",
                Message = "Gasto actualizado",
                Success = true,
                Errors = [],
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}