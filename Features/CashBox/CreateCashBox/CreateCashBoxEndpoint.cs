public static class CreateCashBoxEndpoint
{
    public static void MapCreateCashBox(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (RequestCashBoxCreateDto requestCashBoxCreateDto, RecepcionDbContext db,IAppLogger<string> logger) =>
        {
            var cashBox = new CashBoxMain
            {
                FechaCaja = DateTime.Now,
                FechaHoraApertura = DateTime.Now,
                FechaHoraCierre = null,
                EstadoCaja = "A",
                SaldoInicial = requestCashBoxCreateDto.SaldoInicial,
                SaldoFinal = 0,
                TotalIngreso = 0,
                TotalSalida = 0,
                Observaciones = requestCashBoxCreateDto.Observaciones,
                ObservacionesCierre = "",
                EstadoRegistro = "A",
                BranchSalesId = requestCashBoxCreateDto.BranchSalesId,
                WorkShiftId = requestCashBoxCreateDto.WorkShiftId,
                UserId = requestCashBoxCreateDto.UserId,
            };
            await db.CashBoxMains.AddAsync(cashBox);
            await db.SaveChangesAsync();

            logger.LogInformacion("Caja creada", "CreateCashBoxEndpoint");
            var response = new ApiResponse<int>()
            {
                Success = true,
                Message = "Request was successful",
                Data = cashBox.Id,
                StatusCode = 200
            };

            return Results.Ok(response);

        });
    }
}