public static class GetAccountsPayableEndpoint
{
    public static void MapGetAccountsPayable(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var accountsPayable = await AccountsPayableQueries.GetPendingAsync(db);

            return Results.Ok(new ApiResponse<List<AccountPayableResponse>>
            {
                Success = true,
                Message = "Cuentas por pagar pendientes.",
                StatusCode = StatusCodes.Status200OK,
                Data = accountsPayable
            });
        });
    }
}
