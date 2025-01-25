using Microsoft.EntityFrameworkCore;

public static class CreateCashBoxDetailEndpoint
{

    public static void MapSplitPayCash(this IEndpointRouteBuilder app)
    {
        app.MapPost("/splitPayCash", async (RequestSplitPayCash reqInfoSplitPayDto, RecepcionDbContext db, IAppLogger<string> logger) =>
        {

            var cashBoxDetail = await db.CashBoxDetails.FirstOrDefaultAsync(x => x.Id == reqInfoSplitPayDto.CashBoxDetailId);

            if (cashBoxDetail == null)
            {
                var responseVal = new ApiResponse<int>()
                {
                    Data = 0,
                    Message = "No se encontro detalle de caja",
                    StatusCode = 400,
                    Success = false
                };
                return Results.BadRequest(responseVal);
            }

            foreach (var item in reqInfoSplitPayDto.SplitPayCashDetail)
            {
                var cashBoxDetailSplit = new CashBoxDetail
                {
                    TipoComprobante = cashBoxDetail.TipoComprobante,
                    SerieComprobante = cashBoxDetail.SerieComprobante,
                    NumComprobante = cashBoxDetail.NumComprobante,
                    FechaComprobante = cashBoxDetail.FechaComprobante,
                    Importe = cashBoxDetail.Importe > 0 ? item.Importe : 0,
                    Adelanto = cashBoxDetail.Adelanto > 0 ? item.Importe : 0,
                    TipoPago = item.TipoPago,
                    DescripcionPago = "Ref. pago original ID #" + cashBoxDetail.Id,
                    Observaciones = cashBoxDetail.Observaciones,
                    EstadoRegistro = cashBoxDetail.EstadoRegistro,
                    CustomerId = cashBoxDetail.CustomerId,
                    CashBoxMainId = cashBoxDetail.CashBoxMainId
                };
                await db.CashBoxDetails.AddAsync(cashBoxDetailSplit);
                
            }

            cashBoxDetail.EstadoRegistro = "I";
            cashBoxDetail.Observaciones = "Pago dividido";
            db.CashBoxDetails.Update(cashBoxDetail);
            await db.SaveChangesAsync();

            var response = new ApiResponse<int>()
            {
                Data = cashBoxDetail.Id,
                Message = "División de pago exitosa",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        }).RequireAuthorization();
    }

    public static void MapCreateCashBoxDetail(this IEndpointRouteBuilder app)
    {
        app.MapPost("/detail", async (RequestCashBoxDetailCreateDto requestCashBoxDetailCreateDto, RecepcionDbContext db, IAppLogger<string> logger) =>
        {
            var cashBoxDetail = new CashBoxDetail
            {
                TipoComprobante = requestCashBoxDetailCreateDto.TipoComprobante,
                SerieComprobante = requestCashBoxDetailCreateDto.SerieComprobante,
                NumComprobante = requestCashBoxDetailCreateDto.NumComprobante,
                FechaComprobante = requestCashBoxDetailCreateDto.FechaComprobante,
                Importe = requestCashBoxDetailCreateDto.Importe,
                Adelanto = requestCashBoxDetailCreateDto.Adelanto,
                TipoPago = requestCashBoxDetailCreateDto.TipoPago,
                DescripcionPago = requestCashBoxDetailCreateDto.DescripcionPago,
                Observaciones = requestCashBoxDetailCreateDto.Observaciones,
                EstadoRegistro = requestCashBoxDetailCreateDto.EstadoRegistro,
                CustomerId = requestCashBoxDetailCreateDto.CustomerId,
                CashBoxMainId = requestCashBoxDetailCreateDto.CashBoxMainId
            };
            await db.CashBoxDetails.AddAsync(cashBoxDetail);
            await db.SaveChangesAsync();
            logger.LogInformacion("Request vacio", "CreateCashBoxDetailEndpoint");

            var response = new ApiResponse<int>()
            {
                Data = cashBoxDetail.Id,
                Message = "Request was successful",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }

    public static void MapCreateCashBoxDetailOtros(this IEndpointRouteBuilder app)
    {
        app.MapPost("/detail/otherIn", async (RequestCashBoxDetailCreateOtherInDto requestCashBoxDetailCreateDto, RecepcionDbContext db) =>
        {
            // obtener cashboxMainId por userid
            var cashBoxMain = await db.CashBoxMains
                .FirstOrDefaultAsync(x =>
                x.UserId == requestCashBoxDetailCreateDto.userId
                && x.EstadoRegistro == "A" && x.EstadoCaja == "A");

            if (cashBoxMain == null)
            {
                var responseVal = new ApiResponse<int>()
                {
                    Data = 0,
                    Message = "No se encontro caja abierta para el usuario",
                    StatusCode = 400,
                    Success = false
                };
                return Results.BadRequest(responseVal);
            }

            var cashBoxDetail = new CashBoxDetail
            {
                TipoComprobante = "GS",
                SerieComprobante = requestCashBoxDetailCreateDto.SerieComprobante,
                NumComprobante = requestCashBoxDetailCreateDto.NumComprobante,
                FechaComprobante = new DateTime(),
                Importe = requestCashBoxDetailCreateDto.Importe,
                Adelanto = 0,
                TipoPago = requestCashBoxDetailCreateDto.TipoPago,
                DescripcionPago = requestCashBoxDetailCreateDto.DescripcionPago,
                Observaciones = requestCashBoxDetailCreateDto.Observaciones,
                EstadoRegistro = "A",
                CustomerId = requestCashBoxDetailCreateDto.CustomerId,
                CashBoxMainId = cashBoxMain.Id
            };
            await db.CashBoxDetails.AddAsync(cashBoxDetail);
            await db.SaveChangesAsync();

            var response = new ApiResponse<int>()
            {
                Data = cashBoxDetail.Id,
                Message = "Request was successful",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }

}