using Microsoft.EntityFrameworkCore;

public static class CreateWorkGuideMainEndpoint
{
    public static void MapCreateWorkGuideMain(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (WgmCreateDto wgmCreateDto, RecepcionDbContext db) =>
        {
            if (wgmCreateDto == null)
            {
                return Results.BadRequest("Create data is missing");
            }

            var workGuideMain = new WorkGuideMain
            {
                SerieGuia = wgmCreateDto.SerieGuia,
                NumeroGuia = wgmCreateDto.NumeroGuia,
                FechaOperacion = wgmCreateDto.FechaOperacion,
                FechaHoraEntrega = wgmCreateDto.FechaHoraEntrega,
                MensajeAlertas = wgmCreateDto.MensajeAlertas,
                Observaciones = wgmCreateDto.Observaciones,
                TipoPago = wgmCreateDto.TipoPago,
                DescripcionPago = wgmCreateDto.DescripcionPago,
                TipoRecepcion = wgmCreateDto.TipoRecepcion,
                DireccionContacto = wgmCreateDto.DireccionContacto,
                TelefonoContacto = wgmCreateDto.TelefonoContacto,
                Total = wgmCreateDto.Total,
                Acuenta = wgmCreateDto.Acuenta,
                Saldo = wgmCreateDto.Saldo,
                CustomerId = wgmCreateDto.CustomerId,
                WorkGuideDetails = wgmCreateDto.WorkGuideDetailsDTO.Select(x => new WorkGuideDetail
                {
                    Cant = x.Cant,
                    Precio = x.Precio,
                    Total = x.Total,
                    Observaciones = x.Observaciones,
                    TipoLavado = x.TipoLavado,
                    Ubicacion = x.Ubicacion,
                    EstadoTrabajo = x.EstadoTrabajo,
                    ProductId = x.ProductId,
                    EstadoRegistro = x.EstadoRegistro,
                    EstadoSituacion = x.EstadoSituacion,
                    EstadoPago = x.EstadoPago,
                }).ToList()
            };

            // actualiza el correlativo de la guia de servicio
            var numbersDocument = await db.NumbersDocuments
                .FirstOrDefaultAsync(nd => nd.BranchId == wgmCreateDto.BranchStoreId
                                           && nd.TypeDoc == wgmCreateDto.TypeDocument
                                           && nd.SerieDoc == wgmCreateDto.SerieGuia);
            if (numbersDocument != null)
            {
                numbersDocument.NumberDoc = int.Parse(wgmCreateDto.NumeroGuia) + 1;
                db.NumbersDocuments.Update(numbersDocument);
            }

            // registrar el adelanto en caja
            if (wgmCreateDto.Acuenta > 0)
            {
                var cashBoxMain = await db.CashBoxMains.FirstOrDefaultAsync(c => c.UserId == wgmCreateDto.UserId && c.EstadoRegistro == "A" && c.EstadoCaja == "A");
                if (cashBoxMain == null)
                {
                    return Results.BadRequest("No se encontró caja abierta para el usuario");
                }
                var cashBoxDetail = new CashBoxDetail
                {
                    TipoComprobante = wgmCreateDto.TypeDocument,
                    SerieComprobante = wgmCreateDto.SerieGuia,
                    NumComprobante = wgmCreateDto.NumeroGuia,
                    FechaComprobante = wgmCreateDto.FechaOperacion,
                    Importe = 0,
                    Adelanto = wgmCreateDto.Acuenta,
                    TipoPago = wgmCreateDto.TipoPago,
                    DescripcionPago = wgmCreateDto.DescripcionPago,
                    Observaciones = "Adelanto de guia de servicio",
                    EstadoRegistro = "A",
                    CustomerId = wgmCreateDto.CustomerId,
                    CashBoxMainId = cashBoxMain.Id
                };

                await db.CashBoxDetails.AddAsync(cashBoxDetail);
            }

            await db.WorkGuideMains.AddAsync(workGuideMain);
            await db.SaveChangesAsync();
            var resp = new { IdWorkGuide = workGuideMain.Id };
            return Results.Ok(resp);

        });
    }
}