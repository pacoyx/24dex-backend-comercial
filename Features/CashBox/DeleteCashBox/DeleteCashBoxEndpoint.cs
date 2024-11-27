public static class DeleteCashBoxEndpoint
{
    public static void MapDeleteCashBox(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (int id, RecepcionDbContext db, IAppLogger<string> logger) =>
        {
            if (id == 0)
            {
                return Results.BadRequest("Id must be greater than 0");
            }

            var cashBox = await db.CashBoxMains.FindAsync(id);
            if (cashBox == null)
            {
                logger.LogWarning("CashBox not found", "DeleteCashBoxEndpoint");
                return Results.NotFound();
            }
            db.CashBoxMains.Remove(cashBox);
            await db.SaveChangesAsync();
            var response = new ApiResponse<CashBoxMain>{
                Success = true,
                Message = "Request was successful",
                Data = cashBox,
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}