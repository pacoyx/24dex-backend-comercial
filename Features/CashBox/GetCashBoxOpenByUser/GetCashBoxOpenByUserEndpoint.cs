using Microsoft.EntityFrameworkCore;

public static class GetCashBoxOpenByUserEndpoint
{
    public static void MapGetCashBoxOpenByUser(this IEndpointRouteBuilder app)
    {
        app.MapGet("/cashboxopenbyuser/{userId}", async (int userId, RecepcionDbContext db) =>
        {
            // buscar caja abierta para el usuario
            var openCashBox = await db.CashBoxMains
                .AsNoTracking()
                .Where(cb => cb.UserId == userId && cb.EstadoCaja == "A")
                .FirstOrDefaultAsync();

            if (openCashBox == null)
            {
                return Results.NotFound(new { id = 0, message = "No se encontró caja abierta para el usuario" });
            }

            var cashBoxMainDTO = new ResponseCashBoxOpenDto(
            openCashBox.Id,
            openCashBox.FechaCaja,
            openCashBox.FechaHoraApertura,
            openCashBox.FechaHoraCierre,
            openCashBox.EstadoCaja,
            openCashBox.SaldoInicial,
            openCashBox.SaldoFinal,
            openCashBox.TotalIngreso,
            openCashBox.TotalSalida,
            openCashBox.Observaciones,
            openCashBox.ObservacionesCierre,
            openCashBox.EstadoRegistro,
            openCashBox.BranchSalesId,
            openCashBox.WorkShiftId,
            openCashBox.UserId
            );

            var response = new ApiResponse<ResponseCashBoxOpenDto>(){
                Success = true,
                Message = "Request was successful",
                Data = cashBoxMainDTO,
                StatusCode = 200
            };
            
            return Results.Ok(response);
        });
    }
}