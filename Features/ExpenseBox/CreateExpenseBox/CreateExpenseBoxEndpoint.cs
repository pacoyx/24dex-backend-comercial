using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public static class CreateExpenseBoxEndpoint
{
    public static void MapCreateExpenseBox(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateExpenseBoxDto dto, RecepcionDbContext context) =>
        {         
            var cashBoxMain = await context.CashBoxMains
                .Where(x => x.UserId == dto.UserId && x.EstadoRegistro == "A" && x.EstadoCaja == "A")
                .FirstOrDefaultAsync();

            if (cashBoxMain == null){
                return Results.NotFound("No se encontro caja registrada para el usuario");
            }

            var expenseBox = new ExpenseBox
            {
                CategoryGasto = dto.CategoriaGasto,
                PersonalAutoriza = dto.PersonalAutoriza,
                FechaGasto = DateTime.Now,
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