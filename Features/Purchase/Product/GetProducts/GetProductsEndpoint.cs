public static class GetProductsEndpoint
{
    public static RouteGroupBuilder MapGetProducts(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetProductsService getProductsService) =>
        {
            var products = await getProductsService.GetProductsAsync();
            var response = new ApiResponse<IEnumerable<GetProductsResponseDto>>
            {
                Success = true,
                Data = products,
                StatusCode = StatusCodes.Status200OK,
                Message = "get products"
            };

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<List<GetProductsResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);


        group.MapGet("/{id:int}", async (int id, IGetProductsService getProductsService) =>
        {
            var product = await getProductsService.GetProductAsync(id);
            if (product == null)
            {
                return Results.NotFound(new ApiResponse<GetProductsResponseDto>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Product not found"
                });
            }

            var response = new ApiResponse<GetProductsResponseDto>
            {
                Success = true,
                Data = product,
                StatusCode = StatusCodes.Status200OK,
                Message = "get product"
            };

            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductsResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/search/paginator/{pageNumber:int}/{pageSize:int}", async (int pageNumber, int pageSize, string nameProduct, IGetProductsService getProductsService) =>
        {
            var products = await getProductsService.GetProductsPaginatorAsync(pageNumber, pageSize, nameProduct);
            var response = new ApiResponse<GetProductsResponsePaginatorDto>
            {
                Success = true,
                Data = products,
                StatusCode = StatusCodes.Status200OK,
                Message = "get products paginator"
            };

            return Results.Ok(response);
        })
        .WithName("GetProductsPaginator")
        .Produces<GetProductsResponsePaginatorDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);



        return group;
    }
}