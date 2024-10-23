public static class CreateCashBoxDetailEndpoint
{
    public static void MapCreateCashBoxDetail(this IEndpointRouteBuilder app)
    {
        app.MapPost("/detail", async (RequestCashBoxDetailCreateDto requestCashBoxDetailCreateDto, RecepcionDbContext db) =>
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

            var response = new ApiResponse<int>(){
                Data = cashBoxDetail.Id,
                Message = "Request was successful",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }
}