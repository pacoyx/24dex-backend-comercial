using Microsoft.EntityFrameworkCore;

public static class GetItemsCashBoxDetail
{
    public static void MapGetItemsCashBoxDetail(this IEndpointRouteBuilder app)
    {
        app.MapGet("/detail/{userId}", async (int userId, RecepcionDbContext db) =>
        {
            if (userId == 0)
            {
                return Results.BadRequest("el UserId debe ser diferente de 0");
            }

            // buscar cashboxMain por userId
            var cashBoxMain = await db.CashBoxMains
                .AsNoTracking()
                .Where(cb => cb.UserId == userId && cb.EstadoCaja == "A" && cb.EstadoRegistro == "A")
                .FirstOrDefaultAsync();

            if (cashBoxMain == null)
            {
                return Results.NotFound("No se encontró 'Caja Abierta' para el usuario");
            }

            var cashBoxDetail = await db.CashBoxDetails.AsNoTracking()
                .Include(cbd => cbd.Customer)
                .Where(cbd => cbd.CashBoxMainId == cashBoxMain.Id && cbd.EstadoRegistro == "A")
                .ToListAsync();
            if (cashBoxDetail == null)
            {
                return Results.NotFound();
            }

            var responseDto = cashBoxDetail.Select(cbd => new GetCashBoxDetailResponseDto
            (
                cbd.Id,
                cbd.TipoComprobante,
                cbd.SerieComprobante,
                cbd.NumComprobante,
                cbd.FechaComprobante,
                cbd.Importe,
                cbd.Adelanto,
                cbd.TipoPago,
                cbd.DescripcionPago,
                cbd.Observaciones,
                cbd.Customer ?? new Customer(){ Id = 0, FirtsName = cbd.Observaciones}
            )).ToList();


            var response = new ApiResponse<List<GetCashBoxDetailResponseDto>>()
            {
                Data = responseDto,
                Message = "Request was successful",
                StatusCode = 200,
                Success = true
            };
            return Results.Ok(response);
        });
    }
}