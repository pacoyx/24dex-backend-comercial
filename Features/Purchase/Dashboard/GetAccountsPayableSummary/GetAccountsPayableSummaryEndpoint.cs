public static class GetAccountsPayableSummaryEndpoint
{
    public static void MapGetAccountsPayableSummary(this IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts-payable", async (RecepcionDbContext db) =>
        {
            var accountsPayable = (await AccountsPayableQueries.GetPendingAsync(db))
                .Take(10)
                .ToList();

            return Results.Ok(new ApiResponse<List<AccountPayableResponse>>
            {
                Success = true,
                Message = "Resumen de cuentas por pagar.",
                StatusCode = StatusCodes.Status200OK,
                Data = accountsPayable
            });
        });
    }
}
