using Microsoft.EntityFrameworkCore;

public static class GetWorkGuideMainEndpoint
{
    public static void MapGetWorkGuideMain(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext db) =>
        {

            if (id <= 0){
                return Results.BadRequest("Id must be greater than zero");                
            }

            var workGuideMain = await db.WorkGuideMains
            .Include(w => w.WorkGuideDetails)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(w => w.Id == id);

            if (workGuideMain == null)
            {
                return Results.NotFound();
            }

            var response = new ResponseWgmDto(
                workGuideMain.SerieGuia,
                workGuideMain.NumeroGuia,
                workGuideMain.FechaOperacion,
                workGuideMain.FechaHoraEntrega,
                workGuideMain.MensajeAlertas,
                workGuideMain.Observaciones,
                workGuideMain.TipoPago,
                workGuideMain.DescripcionPago,
                workGuideMain.TipoRecepcion,
                workGuideMain.DireccionContacto,
                workGuideMain.TelefonoContacto,
                workGuideMain.Total,
                workGuideMain.Acuenta,
                workGuideMain.Saldo,
                workGuideMain.CustomerId,
                workGuideMain.WorkGuideDetails.Select(d => new ResponseWgdDto(
                    d.Cant,
                    d.Precio,
                    d.Total,
                    d.Observaciones,
                    d.TipoLavado,
                    d.Ubicacion,
                    d.EstadoTrabajo,
                    d.ProductId,
                    d.Product!,
                    d.EstadoRegistro,
                    d.EstadoSituacion,
                    d.EstadoPago
                ))
            );           
            return Results.Ok(response);
        });
    }
}