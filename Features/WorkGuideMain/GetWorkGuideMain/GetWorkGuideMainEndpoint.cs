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
            .Include(w => w.Customer)
            .Include(w => w.WorkGuideDetails)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(w => w.Id == id);

            if (workGuideMain == null)
            {
                return Results.NotFound();
            }

            var responseWgmDto = new ResponseWgmDto(
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
                workGuideMain.Customer!.FirtsName + ' '+ workGuideMain.Customer.LastName,
                workGuideMain.Customer.Phone,
                workGuideMain.EstadoPago,
                workGuideMain.FechaPago,
                workGuideMain.EstadoRegistro,
                workGuideMain.EstadoSituacion,
                workGuideMain.FechaRecojo,
                workGuideMain.WorkGuideDetails.Select(d => new ResponseWgdDto(
                    d.Id,
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
            var response = new ApiResponse<ResponseWgmDto>(){
                Data = responseWgmDto,
                Message = "WorkGuideMain found",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }
}