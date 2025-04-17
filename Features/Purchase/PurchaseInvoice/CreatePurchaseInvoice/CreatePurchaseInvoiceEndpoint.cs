public static class CreatePurchaseInvoiceEndpoint
{
    public static IEndpointRouteBuilder MapCreatePurchaseInvoice(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", async (ICreatePurchaseInvoiceService service, PurchaseInvoiceRequestDto request) =>
        {
            var response = await service.CreatePurchaseInvoiceAsync(request);
            return Results.Ok(response);
        })
        .WithName("CreatePurchaseInvoice")
        .Produces<PurchaseInvoiceResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithTags("Purchase Invoice");

        return routes;
    }
}