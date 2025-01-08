using Microsoft.EntityFrameworkCore;

public static class GetCashBoxResume
{
    public static void MapGetCashBoxResume(this IEndpointRouteBuilder app)
    {
        app.MapGet("/resume", async (DateTime fecha, int userId, RecepcionDbContext db, IAppLogger<string> logger) =>
        {
            var query = db.CashBoxMains
                .AsNoTracking()
                .Include(c => c.CashBoxDetails)
                .Where(c => c.FechaHoraApertura.Date == fecha.Date && c.UserId == userId);
            var queryWithJoin = from cashBox in query
                                join user in db.Users on cashBox.UserId equals user.Id
                                select new
                                {
                                    cashBox,
                                    user
                                };


            var summary = await queryWithJoin
                .SelectMany(c => c.cashBox.CashBoxDetails!, (c, d) => new { c, d })
                .GroupBy(x => new { x.d.CashBoxMainId, x.d.TipoPago })
                .Select(g => new
                {
                    CajaId = g.Key.CashBoxMainId,
                    Usuario = g.First().c.user.Name,
                    TipoPago = g.Key.TipoPago,
                    TotalAdelanto = g.Sum(d => d.d.Adelanto),
                    TotalImporte = g.Sum(d => d.d.Importe)
                })
                .OrderBy(x => x.Usuario)
                .ToListAsync();



            var dataForResponse = summary.Select(x => new GetCashBoxResumeResponseDto(x.CajaId, x.Usuario, x.TipoPago, x.TotalAdelanto, x.TotalImporte));

            var response = new ApiResponse<IEnumerable<GetCashBoxResumeResponseDto>>
            {
                Data = dataForResponse,
                Message = "Resume de caja",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);

        });
    }

    public static void MapGetCashBoxResumeAllUser(this IEndpointRouteBuilder app)
    {
        app.MapGet("/resumeAllUser", async (DateTime fecha, RecepcionDbContext db, IAppLogger<string> logger) =>
        {
            var query = db.CashBoxMains
                .AsNoTracking()                
                .Where(c => c.FechaHoraApertura.Date == fecha.Date);
            var queryWithJoin = from cashBox in query
                                join user in db.Users on cashBox.UserId equals user.Id
                                select new
                                {
                                    cashBox,
                                    user
                                };
            
            var resultQuery = await queryWithJoin
            .Select(
                x => new
                {
                    x.cashBox.Id,
                    x.user.Name,
                    x.cashBox.FechaHoraApertura,
                    x.cashBox.FechaHoraCierre,
                    x.cashBox.EstadoCaja,
                    x.cashBox.SaldoInicial,
                    x.cashBox.SaldoFinal,
                    x.cashBox.TotalIngreso,
                    x.cashBox.TotalSalida,
                    x.cashBox.Observaciones,
                    x.cashBox.ObservacionesCierre,
                    x.cashBox.EstadoRegistro,
                    x.cashBox.BranchSalesId,
                    x.cashBox.WorkShiftId,
                    x.cashBox.UserId,                    
                }
            )
            .OrderBy(x => x.Name)
            .ToListAsync();

            var dataForResponse = resultQuery.Select(x => new GetInfoCashMain(
                x.Id,
                x.Name,
                x.FechaHoraApertura,
                x.SaldoInicial,
                x.SaldoFinal,
                x.TotalIngreso,
                x.TotalSalida,
                x.EstadoCaja,
                x.UserId
            ));

            var response = new ApiResponse<IEnumerable<GetInfoCashMain>>
            {
                Data = dataForResponse,
                Message = "Resume de caja todos los usuarios",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);


        });
    }

}



//  var query = db.CashBoxMains
//                 .AsNoTracking()
//                 .Include(c => c.CashBoxDetails)
//                 .Where(c => c.FechaHoraApertura.Date == fecha.Date);
//             var queryWithJoin = from cashBox in query
//                                 join user in db.Users on cashBox.UserId equals user.Id
//                                 select new
//                                 {
//                                     cashBox,
//                                     user
//                                 };


//             var summary = await queryWithJoin
//                 .SelectMany(c => c.cashBox.CashBoxDetails!, (c, d) => new { c.user.Name, d })
//                 .GroupBy(x => new { x.Name, x.d.TipoPago })
//                 .Select(g => new
//                 {
//                     Usuario = g.Key.Name,
//                     TipoPago = g.Key.TipoPago,
//                     TotalAdelanto = g.Sum(d => d.d.Adelanto),
//                     TotalImporte = g.Sum(d => d.d.Importe),
//                     Detalle = g.Select(d => new
//                     {
//                         d.d.Adelanto,
//                         d.d.Importe,
//                         d.d.TipoPago,
//                         d.d.CustomerId,
//                         d.d.Customer,
//                         d.d.SerieComprobante,
//                         d.d.NumComprobante
//                     })
//                 })
//                 .OrderBy(x => x.Usuario)
//                 .ToListAsync();

//             var response = new ApiResponse<IEnumerable<GetCashBoxResumeResponseDto>>
//             {
//                 Data = summary.Select(x => new GetCashBoxResumeResponseDto(x.Usuario, x.TipoPago, x.TotalAdelanto, x.TotalImporte,
//                     x.Detalle.Select(d => new GetCashBoxResumeDetalleResponseDto(
//                         d.Adelanto, d.Importe, d.TipoPago, d.CustomerId, d.Customer!.FirtsName + ' ' + d.Customer.LastName,
//                         d.SerieComprobante, d.NumComprobante))
//                         .ToList()
//                         )),
//                 Message = "Resume de caja",
//                 StatusCode = 200,
//                 Success = true
//             };

//             return Results.Ok(response);