using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public static class CreateExpenseBoxEndpoint
{
    public static void MapCreateExpenseBox(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateExpenseBoxDto dto, RecepcionDbContext context, IAppLogger<string> logger) =>
        {         
            var cashBoxMain = await context.CashBoxMains
                .Where(x => x.UserId == dto.UserId && x.EstadoRegistro == "A" && x.EstadoCaja == "A")
                .FirstOrDefaultAsync();

            if (cashBoxMain == null){
                logger.LogWarning("No se encontro caja registrada para el usuario", "CreateExpenseBoxEndpoint");
                return Results.NotFound("No se encontro caja registrada para el usuario");
            }

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

            var expenseBox = new ExpenseBox
            {
                CategoryGasto = dto.CategoriaGasto,
                PersonalAutoriza = dto.PersonalAutoriza,
                FechaGasto = fechaOperacion,
                Importe = dto.Importe,
                DetallesEgreso = dto.DetallesEgreso,
                EstadoRegistro = dto.EstadoRegistro,
                UserId = dto.UserId,
                CashBoxMainId = cashBoxMain.Id
            };
            await context.ExpenseBoxMains.AddAsync(expenseBox);
            await context.SaveChangesAsync();

            var response = new ApiResponse<ExpenseBox>{
                Data = expenseBox,
                Message = "Gasto creado",
                StatusCode = 200,
                Errors = [],
                Success = true
            };

            return Results.Ok(response);
        });
    }
}