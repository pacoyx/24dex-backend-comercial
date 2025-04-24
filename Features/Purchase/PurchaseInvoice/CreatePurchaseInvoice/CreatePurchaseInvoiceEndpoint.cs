public static class CreatePurchaseInvoiceEndpoint
{
    public static void MapCreatePurchaseInvoice(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", async (ICreatePurchaseInvoiceService service, PurchaseInvoiceRequestDto request) =>
        {
           
            var responseInvoice = await service.CreatePurchaseInvoiceAsync(request);
            var response = new ApiResponse<PurchaseInvoiceResponseDto>
            {
                Success = true,
                Data = responseInvoice,
                StatusCode = StatusCodes.Status201Created,
                Message = "Purchase invoice created successfully."
            };

            return Results.Ok(response);            
        })
        .WithName("CreatePurchaseInvoice")
        .Produces<PurchaseInvoiceResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithTags("Purchase Invoice");        
    }
}