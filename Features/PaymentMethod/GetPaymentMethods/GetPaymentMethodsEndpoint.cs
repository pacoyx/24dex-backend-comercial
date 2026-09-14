using Microsoft.EntityFrameworkCore;

public static class GetPaymentMethodsEndpoint
{
    public static void MapGetPaymentMethods(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var paymentMethods = await db.PaymentMethods
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new PaymentMethodResponse(x.Id, x.Name, x.IsActive))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<PaymentMethodResponse>>
            {
                Success = true,
                Message = "Listado de métodos de pago.",
                StatusCode = StatusCodes.Status200OK,
                Data = paymentMethods
            });
        });
    }
}
