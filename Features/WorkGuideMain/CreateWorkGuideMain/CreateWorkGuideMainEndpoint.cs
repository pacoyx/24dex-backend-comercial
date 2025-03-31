
using System.Text.Json;
using Microsoft.EntityFrameworkCore;


public static class CreateWorkGuideMainEndpoint
{
    public static void MapCreateWorkGuideMain(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (WgmCreateDto wgmCreateDto, RecepcionDbContext db, IAppLogger<string> logger) =>
        {

            if (wgmCreateDto == null)
            {
                logger.LogWarning("Request vacio", "CreateWorkGuideMainEndpoint");
                return Results.BadRequest("Request vacio");
            }

            var TYPE_PROCESS = "RECEP";

            //obtener el ultimo correlativo de la guia de servicio
            var numbersDocument = await db.NumbersDocuments
                .FirstOrDefaultAsync(nd => nd.BranchId == wgmCreateDto.BranchStoreId
                                        && nd.TypeProcess == TYPE_PROCESS);
            if (numbersDocument == null)
            {
                logger.LogWarning("No se encontró correlativo de guia de servicio", "CreateWorkGuideMainEndpoint");
                return Results.BadRequest("No se encontró correlativo de guia de servicio");
            }

            //validar si el correlativo de la guia de servicio es igual al enviado
            // if (numbersDocument.NumberDoc != int.Parse(wgmCreateDto.NumeroGuia))
            // {
            //     logger.LogWarning("El correlativo de la guia de servicio no coincide", "CreateWorkGuideMainEndpoint");
            //     return Results.BadRequest("El correlativo de la guia de servicio no coincide");
            // }

            //validar si el cliente existe
            var customer = await db.Customers.FirstOrDefaultAsync(c => c.Id == wgmCreateDto.CustomerId);
            if (customer == null)
            {
                logger.LogWarning("No se encontró cliente", "CreateWorkGuideMainEndpoint");
                return Results.BadRequest("No se encontró cliente");
            }


            string identifierLog = Guid.NewGuid().ToString();

            DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            var workGuideMain = new WorkGuideMain
            {
                SerieGuia = numbersDocument.SerieDoc,
                NumeroGuia = numbersDocument.NumberDoc.ToString(), //wgmCreateDto.NumeroGuia,
                FechaOperacion = fechaOperacion,
                FechaHoraEntrega = fechaOperacion.AddDays(2), // Add 2 days to the current date
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

                EstadoPago = wgmCreateDto.EstadoPago,
                FechaPago = wgmCreateDto.EstadoPago == "PA" ? fechaOperacion : null,
                EstadoRegistro = wgmCreateDto.EstadoRegistro,
                EstadoSituacion = wgmCreateDto.EstadoSituacion,
                FechaRecojo = null,

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
                    Identificador = x.Identificador
                }).ToList()
            };

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == wgmCreateDto.UserId);
            if (user == null)
            {
                logger.LogInformacion($"Usuario con {wgmCreateDto.UserId} no encontrado");
            }


            // registrar el adelanto en caja
            if (wgmCreateDto.Acuenta > 0)
            {
                var cashBoxMain = await db.CashBoxMains.FirstOrDefaultAsync(c => c.UserId == wgmCreateDto.UserId && c.EstadoRegistro == "A" && c.EstadoCaja == "A");
                if (cashBoxMain == null)
                {
                    logger.LogWarning("No se encontró caja abierta para el usuario", "CreateWorkGuideMainEndpoint");
                    return Results.BadRequest("No se encontró caja abierta para el usuario");
                }
                var cashBoxDetail = new CashBoxDetail
                {
                    TipoComprobante = numbersDocument!.TypeDoc,
                    SerieComprobante = numbersDocument!.SerieDoc,
                    NumComprobante = numbersDocument!.NumberDoc.ToString(),
                    FechaComprobante = fechaOperacion,
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

                var dataCaja = new
                {
                    cajaId = cashBoxMain.Id,
                    fecha = fechaOperacion,
                    clienteId = wgmCreateDto.CustomerId,
                    comprobante = numbersDocument!.TypeDoc + " " + numbersDocument!.SerieDoc + " " + numbersDocument!.NumberDoc,
                    tipoPago = wgmCreateDto.TipoPago,
                    monto = wgmCreateDto.Acuenta,
                };

                var cajaString = JsonSerializer.Serialize(dataCaja);
                logger.LogMessageWithEventAndId($"Adelanto de caja, por {user!.UserName}", 1001, identifierLog, cajaString);
            }

            await db.WorkGuideMains.AddAsync(workGuideMain);

            // actualiza el correlativo de la guia de servicio            
            if (numbersDocument != null)
            {
                numbersDocument.NumberDoc = numbersDocument.NumberDoc + 1;
                db.NumbersDocuments.Update(numbersDocument);
            }

            await db.SaveChangesAsync();

            var resp = new WgmCreateResponseDto(            
                 workGuideMain.Id,
                 numbersDocument!.TypeDoc,
                 workGuideMain.SerieGuia,
                 workGuideMain.NumeroGuia,
                 workGuideMain.FechaOperacion,
                 workGuideMain.FechaHoraEntrega
            );


            var dataGuiaLog = new
            {
                idGuia = workGuideMain.Id,
                comprobante = workGuideMain.SerieGuia + " " + workGuideMain.NumeroGuia,
                fechaOperacion = workGuideMain.FechaOperacion,
                clienteId = workGuideMain.CustomerId,
                total = workGuideMain.Total,
                acuenta = workGuideMain.Acuenta,
                saldo = workGuideMain.Saldo,
                detalles = workGuideMain.WorkGuideDetails.Select(x => new
                {
                    productoId = x.ProductId,
                    cantidad = x.Cant,
                    precio = x.Precio,
                    total = x.Total,
                    observaciones = x.Observaciones
                })
            };
            var guiaString = JsonSerializer.Serialize(dataGuiaLog);
            logger.LogMessageWithEventAndId($"Guia creada por {user!.UserName} ", 1001, identifierLog, guiaString);



            var response = new ApiResponse<WgmCreateResponseDto>
            {
                Data = resp,
                Success = true,
                Message = "",
                StatusCode = 200
            };


            return Results.Ok(response);
        });
    }
}