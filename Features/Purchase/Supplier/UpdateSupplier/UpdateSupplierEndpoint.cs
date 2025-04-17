public static class UpdateSupplierEndpoint
{
    public static IEndpointRouteBuilder MapUpdateSupplier(this IEndpointRouteBuilder routes)
    {
        routes.MapPut("/", async (IUpdateSupplierService service, UpdateSupplierRequest request) =>
        {
            var responseService = await service.UpdateSupplierAsync(request);
            if (responseService != "OK")
            {
                return Results.BadRequest(responseService);
            }            

            var response = new ApiResponse<string>
            {
                Success = true,
                Data = responseService,
                StatusCode = StatusCodes.Status200OK,
                Message = "Supplier updated successfully."
            };

            return Results.Ok(response);
        })
        .WithName("UpdateSupplier")
        .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
        .Produces<ApiResponse<string>>(StatusCodes.Status400BadRequest)
        .WithTags("Supplier");

        return routes;
    }
}