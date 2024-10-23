using Microsoft.EntityFrameworkCore;

public static class GetCashBoxesEndpoint
{
    public static void MapGetCashBoxes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var cashBoxes = await db.CashBoxMains
                .Include(cb => cb.CashBoxDetails!)
                    .ThenInclude(detail => detail.Customer)
                .ToListAsync();

            if (cashBoxes == null)
            {
                return Results.NotFound();
            }

            var response = cashBoxes.Select(x => new ResponseCashBoxMainAllDTO(
             x.Id,
             x.FechaCaja,
             x.FechaHoraApertura,
             x.FechaHoraCierre,
             x.EstadoCaja,
             x.SaldoInicial,
             x.SaldoFinal,
             x.TotalIngreso,
             x.TotalSalida,
             x.Observaciones,
             x.ObservacionesCierre,
             x.EstadoRegistro,
             x.BranchSalesId,
             x.WorkShiftId,
             x.UserId,
                x.CashBoxDetails?.Select(d => new ResponseCashBoxDetailAllDTO(
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
                    d.Customer!,
                    d.CashBoxMainId
                ))
            ));

            return Results.Ok(response);

        });
    }
}