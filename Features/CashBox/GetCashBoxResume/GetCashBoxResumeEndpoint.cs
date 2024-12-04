using Microsoft.EntityFrameworkCore;

public static class GetCashBoxResume{
    public static void MapGetCashBoxResume(this IEndpointRouteBuilder app)
    {
        app.MapGet("/resume", async (DateTime fecha, RecepcionDbContext db, IAppLogger<string> logger) =>
        {            
            var query = db.CashBoxMains
                .AsNoTracking()
                .Include(c => c.CashBoxDetails)                
                .Where(c => c.FechaHoraApertura.Date == fecha.Date);
            var queryWithJoin = from cashBox in query
                                join user in db.Users on cashBox.UserId equals user.Id
                                select new
                                {
                                    cashBox,
                                    user
                                };
            
            
            var summary = await queryWithJoin
                .SelectMany(c => c.cashBox.CashBoxDetails!, (c, d) => new { c.user.Name, d })
                .GroupBy(x => new { x.Name, x.d.TipoPago })
                .Select(g => new 
                {
                    Usuario = g.Key.Name,
                    TipoPago = g.Key.TipoPago,
                    TotalAdelanto = g.Sum(d => d.d.Adelanto),
                    TotalImporte = g.Sum(d => d.d.Importe)
                })
                .OrderBy(x => x.Usuario)
                .ToListAsync();


            var response = new ApiResponse<IEnumerable<GetCashBoxResumeResponseDto>>{
                Data = summary.Select(x => new GetCashBoxResumeResponseDto(x.Usuario, x.TipoPago, x.TotalAdelanto, x.TotalImporte)),
                Message = "Resume de caja",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);

        });
    }
}