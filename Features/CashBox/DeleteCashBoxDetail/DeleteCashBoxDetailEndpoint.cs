public static class DeleteCashBoxDetailEndpoint
{
    public static void MapDeleteCashBoxDetail(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/detail/{id}", async (int id, RecepcionDbContext db,IAppLogger<string> logger) =>
        {
            if (id == 0)
            {
                logger.LogWarning("Id must be greater than 0", "DeleteCashBoxDetailEndpoint");
                var responseValidation = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Id must be greater than 0",
                    Data = "",
                    StatusCode = 400
                };
                return Results.BadRequest(responseValidation);
            }

            var cashBoxDetail = await db.CashBoxDetails.FindAsync(id);
            if (cashBoxDetail == null)
            {
                logger.LogWarning("CashBoxDetail not found", "DeleteCashBoxDetailEndpoint");
                return Results.NotFound();
            }

            db.CashBoxDetails.Remove(cashBoxDetail);
            await db.SaveChangesAsync();

            logger.LogInformacion("CashBoxDetail deleted successfully" + id.ToString(), "DeleteCashBoxDetailEndpoint");
            var response = new ApiResponse<CashBoxDetail>
            {
                Success = true,
                Message = "Request was successful",
                Data = cashBoxDetail,
                StatusCode = 200
            };

            return Results.Ok(response);
        });
    }
}