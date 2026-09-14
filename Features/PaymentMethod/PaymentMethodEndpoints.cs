public static class PaymentMethodEndpoints
{
    public static void MapPaymentMethod(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payment-methods").WithTags("PaymentMethods");
        group.MapGetPaymentMethods();
    }
}
