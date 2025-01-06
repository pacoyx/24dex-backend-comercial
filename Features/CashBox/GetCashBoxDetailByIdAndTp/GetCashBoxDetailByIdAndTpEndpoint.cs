using Microsoft.EntityFrameworkCore;

public static class GetCashBoxDetailByIdAndTpEndpoint
{
    public static void MapGetCashBoxDetailByIdAndTp(this IEndpointRouteBuilder app)
    {
        app.MapGet("/detail/{cajaId}/{tp}", async (int cajaId, string tp, RecepcionDbContext db) =>
        {
            if (cajaId == 0)
            {
                return Results.BadRequest("el UserId debe ser diferente de 0");
            }

            
            // var cashBoxMain = await db.CashBoxMains
            //     .AsNoTracking()
            //     .Where(cb => cb.Id == cajaId && cb.EstadoRegistro == "A")
            //     .FirstOrDefaultAsync();

            // if (cashBoxMain == null)
            // {
            //     return Results.NotFound("No se encontró 'Caja Abierta' para el usuario");
            // }

            // var infoCashBoxMain = new GetInfoCashMain(
            //     cashBoxMain.FechaHoraApertura,
            //     cashBoxMain.SaldoInicial,
            //     cashBoxMain.SaldoFinal,
            //     cashBoxMain.EstadoCaja
            // );


            var cashBoxDetail = await db.CashBoxDetails.AsNoTracking()
                .Include(cbd => cbd.Customer)
                .Where(cbd => cbd.CashBoxMainId == cajaId && cbd.TipoPago == tp)
                .ToListAsync();
            if (cashBoxDetail == null)
            {
                return Results.NotFound();
            }

            var expenseBox = new List<ExpenseBox>();
            if (tp == "EF")
            {
                expenseBox = await db.ExpenseBoxMains.AsNoTracking()
                       .Where(eb => eb.CashBoxMainId == cajaId)
                       .ToListAsync();
            }


            var responseDto = cashBoxDetail.Select(cbd => new GetCashBoxDetalleByIdyTpResponseDto(
                cbd.Adelanto,
                cbd.Importe,
                cbd.TipoPago,
                cbd.CustomerId,
                cbd.Customer != null ? cbd.Customer.FirtsName + ' ' + cbd.Customer.LastName : cbd.Observaciones,
                cbd.SerieComprobante,
                cbd.NumComprobante,
                "processando..."
            )).ToList();

            var responseDto2 = expenseBox.Select(eb => new GetExpeseBoxByIdCashResponseDto(
                eb.FechaGasto,
                eb.Importe,
                eb.DetallesEgreso
            )).ToList();

            var responseDto3 = new GetCashBoxDetailByIdAndTpResponseDto(responseDto, responseDto2);

            var response = new ApiResponse<GetCashBoxDetailByIdAndTpResponseDto>
            {
                Data = responseDto3,
                Message = "Request was successful",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }

}