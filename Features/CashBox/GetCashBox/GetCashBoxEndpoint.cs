using Microsoft.EntityFrameworkCore;

public static class GetCashBoxEndpoint
{
    public static void MapGetCashBox(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (RecepcionDbContext db, int id) =>
        {

            if (id <= 0){
                return Results.BadRequest("Id must be greater than zero");
            }

            var cashBox = await db.CashBoxMains
               .Include(cb => cb.CashBoxDetails!)
                   .ThenInclude(detail => detail.Customer)
               .FirstOrDefaultAsync(cb => cb.Id == id);

            if (cashBox == null)
            {
                return Results.NotFound();
            }

            var response = new ResponseCashBoxDto(
                cashBox.Id,
                cashBox.FechaCaja,
                cashBox.FechaHoraApertura,
                cashBox.FechaHoraCierre,
                cashBox.EstadoCaja,
                cashBox.SaldoInicial,
                cashBox.SaldoFinal,
                cashBox.TotalIngreso,
                cashBox.TotalSalida,
                cashBox.Observaciones,
                cashBox.ObservacionesCierre,
                cashBox.EstadoRegistro,
                cashBox.BranchSalesId,
                cashBox.WorkShiftId,
                cashBox.UserId,
                cashBox.CashBoxDetails?.Select(d => new ResponseCashBoxDetailDto(
                    d.Id,
                    d.TipoComprobante,
                    d.SerieComprobante,
                    d.NumComprobante,
                    d.FechaComprobante,
                    d.Importe,
                    d.Adelanto,
                    d.TipoPago,
                    d.DescripcionPago,
                    d.Observaciones,
                    d.EstadoRegistro,
                    d.Customer,
                    d.CashBoxMainId
                ))
            );

            return Results.Ok(response);

        });
    }
}