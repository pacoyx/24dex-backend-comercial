public static class GetPurchasePaymentsEndpoint
{
    public static IEndpointRouteBuilder MapGetPurchasePayments(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/{id:int}/payments", async (int id, IGetPurchasePaymentsService service) =>
        {
            var payments = await service.GetPaymentsAsync(id);
            if (payments == null)
            {
                return Results.NotFound(new ApiResponse<List<RegisterPurchasePaymentResponseDto>>
                {
                    Success = false,
                    Message = "Compra no encontrada."
                });
            }

            var response = new ApiResponse<List<RegisterPurchasePaymentResponseDto>>
            {
                Success = true,
                Message = "Pagos de la compra.",
                StatusCode = StatusCodes.Status200OK,
                Data = payments
            };

            return Results.Ok(response);
        })
        .WithName("GetPurchasePayments")
        .Produces<List<RegisterPurchasePaymentResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Purchase Invoice");

        return routes;
    }
}
