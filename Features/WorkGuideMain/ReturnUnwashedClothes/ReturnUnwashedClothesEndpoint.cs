using Microsoft.EntityFrameworkCore;

public static class ReturnUnwashedClothesEndpoint
{
    public static void MapReturnUnwashedClothes(this IEndpointRouteBuilder app)
    {
        app.MapPut("/returnUnwashedClothes/{id}", async (RecepcionDbContext db, int id, RequestDevolucionDataDto request) =>
        {
            var workGuideItem = await db.WorkGuideDetails.FindAsync(id);
            if (workGuideItem == null)
            {
                var responseValidation = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "Item no encontrado",
                    StatusCode = 404,
                    Success = false
                };
                return Results.NotFound(responseValidation);
            }

            if (workGuideItem.EstadoSituacion == "D")
            {
                var responseValidation = new ApiResponse<string>()
                {
                    Data = "",
                    Message = "El item ya se encuentra devuelto",
                    StatusCode = 400,
                    Success = false
                };
                return Results.BadRequest(responseValidation);
            }

            workGuideItem.EstadoSituacion = "D";


            // si el monto es mayor a 0 se registra en ExpenseBox
            if (request.DevolverEfectivo && request.Monto > 0)
            {
                // buscar guia de trabajo por id del item y resta el monto del campos saldo
                var workGuide = await db.WorkGuideMains.FindAsync(workGuideItem.WorkGuideMainId);
                if (workGuide == null)
                {
                    var responseValidation = new ApiResponse<string>()
                    {
                        Data = "",
                        Message = "No se encontro guia de trabajo",
                        StatusCode = 404,
                        Success = false
                    };
                    return Results.NotFound(responseValidation);
                }
                workGuide.Saldo = workGuide.Saldo - request.Monto;

                // obtener el id de caja principal por el iduser
                var cashBoxMain = await db.CashBoxMains.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.EstadoRegistro == "A" && x.EstadoCaja == "A");
                if (cashBoxMain == null)
                {
                    var responseValidation = new ApiResponse<string>()
                    {
                        Data = "",
                        Message = "No se encontro caja abierta para el usuario",
                        StatusCode = 404,
                        Success = false
                    };
                    return Results.NotFound(responseValidation);
                }

                // registrar devolucion de efectivo en ExpenseBox
                var expenseBox = new ExpenseBox()
                {
                    CategoryGasto = "Devolucion de ropa sin lavar",
                    PersonalAutoriza = "Sistema",
                    FechaGasto = DateTime.Now,
                    Importe = (decimal)request.Monto,
                    DetallesEgreso = "Devolucion de ropa sin lavar",
                    EstadoRegistro = "A",
                    UserId = request.UserId,
                    CashBoxMainId = cashBoxMain.Id
                };
                db.ExpenseBoxMains.Add(expenseBox);
            }

            await db.SaveChangesAsync();
            var response = new ApiResponse<string>()
            {
                Data = "El item fue actualizado como DEVUELTO con exito",
                Message = "El item fue actualizado como DEVUELTO con exito",
                Success = true,
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}