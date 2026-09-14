using Microsoft.EntityFrameworkCore;

public static class GetExpensesBoxByDateEndpoint
{
    public static void MapGetExpensesBoxByDate(this IEndpointRouteBuilder app)
    {
        app.MapGet("/byDate/{date}", async (DateTime date, RecepcionDbContext context) =>
        {
            var expenseBox = await context.ExpenseBoxMains.AsNoTracking()
                .Where(x => x.FechaGasto.Date == date.Date)
                .OrderByDescending(x => x.FechaGasto)
                .Select(x => new GetExpensesBoxDtoResponse(
                    x.Id,
                    x.CategoryGasto,
                    x.PersonalAutoriza,
                    x.FechaGasto,
                    x.Importe,
                    x.DetallesEgreso,
                    x.EstadoRegistro,
                    x.UsuarioRegistro
                ))
                .ToListAsync();

            var response = new ApiResponse<List<GetExpensesBoxDtoResponse>>()
            {
                Data = expenseBox,
                Message = "Lista de gastos por fecha",
                StatusCode = 200,
                Errors = [],
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
