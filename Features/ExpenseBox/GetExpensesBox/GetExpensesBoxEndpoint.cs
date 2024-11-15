using Microsoft.EntityFrameworkCore;

public static class GetExpensesBoxEndpoint
{
    public static void MapGetExpensesBox(this IEndpointRouteBuilder app)
    {
        app.MapGet("/byUser/{userId}", async (int userId, RecepcionDbContext context) =>
        {
            if (userId == 0)
            {
                return Results.BadRequest("El id del usuario no puede ser 0");
            }

            // obetner cashboxmainID por userId
            var cashBoxMain = await context.CashBoxMains.AsNoTracking()
                .Where(x => x.UserId == userId && x.EstadoRegistro == "A" && x.EstadoCaja == "A")
                .FirstOrDefaultAsync();

            if (cashBoxMain == null)
            {
                return Results.NotFound("No se encontro 'Caja Abierta' registrada para el usuario");
            }

            var expenseBox = await context.ExpenseBoxMains.AsNoTracking()
            .Where(x => x.UserId == userId && x.CashBoxMainId == cashBoxMain.Id)
            .Select(x => new GetExpensesBoxDtoResponse(
                x.Id,
                x.CategoryGasto,
                x.PersonalAutoriza,
                x.FechaGasto,
                x.Importe,
                x.DetallesEgreso,
                x.EstadoRegistro
            ))
            .ToListAsync();

            var response = new ApiResponse<List<GetExpensesBoxDtoResponse>>()
            {
                Data = expenseBox,
                Message = "Lista de gastos",
                StatusCode = 200,
                Errors = [],
                Success = true
            };

            return Results.Ok(response);
        });
    }
}