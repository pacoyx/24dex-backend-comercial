public static class GetCategoriesProdEndpoint
{
    public static RouteGroupBuilder MapGetCategoriesProd(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IGetCategoriesProdService getCategoriesProdService) =>
        {
            var categories = await getCategoriesProdService.GetCategoriesProdAsync();            
            var response = new ApiResponse<IEnumerable<GetCategoriesProdResponse>>
            {
                Success = true,
                Data = categories,
                StatusCode = StatusCodes.Status200OK,
                Message = "get categories prod"
            };

            return Results.Ok(response);
        })        
        .WithName("GetCategoriesProd")
        .CacheOutput()
        .Produces<List<GetCategoriesProdResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }

    public static RouteGroupBuilder MapGetCategoriesProdShort(this RouteGroupBuilder group)
    {
        group.MapGet("/short", async (IGetCategoriesProdService getCategoriesProdService) =>
        {
            var categories = await getCategoriesProdService.GetCategoriesProdShortAsync();
            var response = new ApiResponse<IEnumerable<GetCategoriesProdShortResponse>>
            {
                Success = true,
                Data = categories,
                StatusCode = StatusCodes.Status200OK,
                Message = "get categories prod short"
            };
            return Results.Ok(response);
        })
        .WithName("GetCategoriesProdShort")
        .Produces<List<GetCategoriesProdShortResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }
}