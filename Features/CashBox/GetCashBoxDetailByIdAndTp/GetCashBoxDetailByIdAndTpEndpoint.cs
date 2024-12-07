using Microsoft.EntityFrameworkCore;

public static class GetCashBoxDetailByIdAndTpEndpoint
{
    public static void MapGetCashBoxDetailByIdAndTp(this IEndpointRouteBuilder app)
    {
        app.MapGet("/detail/{cajaId}/{tp}", async (int cajaId, string tp, RecepcionDbContext db) =>
        {
            if (cajaId == 0)
            {
                return Results.BadRequest("el UserId debe ser diferente de 0");
            }

            // buscar cashboxMain por userId
            // var cashBoxMain = await db.CashBoxMains
            //     .AsNoTracking()
            //     .Where(cb => cb.Id == cajaId && cb.EstadoRegistro == "A")
            //     .FirstOrDefaultAsync();

            // if (cashBoxMain == null)
            // {
            //     return Results.NotFound("No se encontró 'Caja Abierta' para el usuario");
            // }

            var cashBoxDetail = await db.CashBoxDetails.AsNoTracking()
                .Include(cbd => cbd.Customer)
                .Where(cbd => cbd.CashBoxMainId == cajaId && cbd.TipoPago == tp)
                .ToListAsync();
            if (cashBoxDetail == null)
            {
                return Results.NotFound();
            }

            var responseDto = cashBoxDetail.Select(cbd => new GetCashBoxDetalleByIdyTpResponseDto(
                cbd.Adelanto,
                cbd.Importe,
                cbd.TipoPago,
                cbd.CustomerId,
                cbd.Customer != null ? cbd.Customer.FirtsName + ' ' +cbd.Customer.LastName : cbd.Observaciones,
                cbd.SerieComprobante,
                cbd.NumComprobante,
                "processando..."
            )).ToList();

            var response = new ApiResponse<List<GetCashBoxDetalleByIdyTpResponseDto>>{
                Data = responseDto,
                Message = "Request was successful",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }

}