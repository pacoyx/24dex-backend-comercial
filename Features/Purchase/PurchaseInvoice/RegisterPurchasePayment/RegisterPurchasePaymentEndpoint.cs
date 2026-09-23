using FluentValidation;

public static class RegisterPurchasePaymentEndpoint
{
    public static IEndpointRouteBuilder MapRegisterPurchasePayment(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/{id:int}/payments", async (
            int id,
            RegisterPurchasePaymentRequestDto request,
            IValidator<RegisterPurchasePaymentRequestDto> validator,
            IRegisterPurchasePaymentService service) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var result = await service.RegisterPaymentAsync(id, request);
            if (result.NotFound)
            {
                return Results.NotFound(new ApiResponse<RegisterPurchasePaymentResponseDto>
                {
                    Success = false,
                    Message = result.Error ?? "Compra no encontrada."
                });
            }

            if (result.Error is not null)
            {
                return Results.BadRequest(new ApiResponse<RegisterPurchasePaymentResponseDto>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            var response = new ApiResponse<RegisterPurchasePaymentResponseDto>
            {
                Success = true,
                Message = "Pago registrado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = result.Data
            };

            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("RegisterPurchasePayment")
        .Produces<RegisterPurchasePaymentResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Purchase Invoice");

        return routes;
    }
}
