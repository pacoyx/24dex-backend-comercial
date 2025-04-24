public static class UpdateProductEndpoint
{
    public static void MapUpdateProduct(this IEndpointRouteBuilder app)
    {
        app.MapPut("/", async (UpdateProductRequestDto request, IUpdateProductService updateProductService) =>
        {
            var result = await updateProductService.UpdateProductAsync(request);
            if (result != "OK")
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = result,
                    Data = string.Empty
                });
            }            

            var response = new ApiResponse<string>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Product updated successfully"
            };

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Products");
    }
}