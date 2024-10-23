public static class CloseCashBoxEndpoint
{
    public static void MapCloseCashBox(this IEndpointRouteBuilder app)
    {
        app.MapPut("/close/{id}", async (int id, RequestCashBoxCloseDto requestCashBoxCloseDto, RecepcionDbContext db) =>
        {
            if (id == 0)
            {
                return Results.BadRequest("Id must be greater than 0");
            }

            if (requestCashBoxCloseDto == null)
            {
                return Results.BadRequest("No se envió el estado de cierre de caja");
            }

            if (id != requestCashBoxCloseDto.Id)
            {
                return Results.BadRequest("El id de caja no coincide con el id de caja enviado");
            }

            var estado = requestCashBoxCloseDto.EstadoCaja;
            if (string.IsNullOrEmpty(estado))
            {
                return Results.BadRequest("No se envió el estado de cierre de caja");
            }

            if (estado != "C")
            {
                return Results.BadRequest("El estado de cierre de caja debe ser 'C'");
            }

            if (requestCashBoxCloseDto.SaldoFinal < 0)
            {
                return Results.BadRequest("El saldo final de caja debe ser mayor a 0");
            }


            var cashBox = await db.CashBoxMains.FindAsync(id);
            if (cashBox == null)
            {
                return Results.NotFound();
            }
            Console.WriteLine("Estado de caja: " + cashBox.EstadoCaja);

            if (cashBox.EstadoCaja == "C")
            {
                return Results.BadRequest("La caja ya fue cerrada");
            }

            cashBox.FechaHoraCierre = DateTime.Now;
            cashBox.EstadoCaja = estado;
            cashBox.SaldoFinal = requestCashBoxCloseDto.SaldoFinal;
            cashBox.TotalIngreso = requestCashBoxCloseDto.TotalIngreso;
            cashBox.TotalSalida = requestCashBoxCloseDto.TotalSalida;
            cashBox.ObservacionesCierre = requestCashBoxCloseDto.ObservacionesCierre;

            await db.SaveChangesAsync();

            var response = new ApiResponse<CashBoxMain>(){
                Data = cashBox,
                Message = "Request was successful",
                StatusCode = 200,
                Success = true
            };
            
            return Results.Ok(response);
        });
    }
}