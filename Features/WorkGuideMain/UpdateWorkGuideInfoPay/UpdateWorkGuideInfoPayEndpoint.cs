using Microsoft.EntityFrameworkCore;

public static class UpdateWorkGuideInfoPayEndpoint
{
    public static void MapUpdateWorkGuideInfoPay(this IEndpointRouteBuilder app)
    {
        app.MapPut("/updateWorkGuideInfoPay/{id}", async (RecepcionDbContext db, int id, UpdateWorkGuideInfoPayRequestDto request) =>
        {
            var workGuide = await db.WorkGuideMains.FindAsync(id);
            if (workGuide == null)
            {
                var responseErr = new ApiResponse<string>
                {
                    Data = "WorkGuide not found",
                    Message = "WorkGuide not found",
                    Success = false,
                    StatusCode = 404,
                    Errors = new List<string> { "guia no se encuentra" }
                };
                return Results.NotFound(responseErr);
            }

            workGuide.TipoPagoCancelacion = request.TipoPago;            
            workGuide.FechaPago = DateTime.Now;
            workGuide.EstadoPago = request.EstadoPago;

            // registramos el pago en caja
            var cashBoxMain = await db.CashBoxMains.FirstOrDefaultAsync(c => c.UserId == request.idUser && c.EstadoRegistro == "A" && c.EstadoCaja == "A");
            if (cashBoxMain == null)
            {
                return Results.BadRequest();
            }

            var cashBoxDetail = new CashBoxDetail
            {
                TipoComprobante = "GS",
                SerieComprobante = workGuide.SerieGuia,
                NumComprobante = workGuide.NumeroGuia,
                FechaComprobante = workGuide.FechaOperacion,
                Importe = workGuide.Saldo,
                Adelanto = 0,
                TipoPago = request.TipoPago,
                DescripcionPago = request.DescripcionPago,
                Observaciones = "",
                EstadoRegistro = "A",
                CustomerId = workGuide.CustomerId,
                CashBoxMainId = cashBoxMain.Id
            };

            await db.CashBoxDetails.AddAsync(cashBoxDetail);
            await db.SaveChangesAsync();

            var response = new ApiResponse<DateTime?>
            {
                Data = workGuide.FechaPago,
                Message = "se actualizo el pago de la guia de servicio",
                Success = true,
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}