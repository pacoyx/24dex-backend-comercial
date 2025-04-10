public static class CreateCategoryProdEndpoint
{
    public static RouteGroupBuilder MapCreateCategoryProd(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateCategoryProdDto createCategoryProdDto, ICreateCategoryProdService createCategoryProdService) =>
        {
            var categoryProd = await createCategoryProdService.CreateCategoryProdAsync(createCategoryProdDto);
            var response = new ApiResponse<CreateCategoryProdResponseDto>
            {
                Success = true,
                Data = categoryProd,
                StatusCode = StatusCodes.Status200OK,
                Message = ""
            };

            return Results.Ok(response);
        })
        .WithName("CreateCategoryProd")
        .Produces<CreateCategoryProdResponseDto>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Purchase");

        return group;
    }
}