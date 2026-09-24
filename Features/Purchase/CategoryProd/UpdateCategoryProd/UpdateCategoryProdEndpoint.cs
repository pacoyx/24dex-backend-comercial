public static class UpdateCategoryProdEndpoint
{
    public static RouteGroupBuilder MapUpdateCategoryProd(this RouteGroupBuilder group)
    {
        group.MapPut("/", async (UpdateCategoryProdRequestDto request, IUpdateCategoryProdService service) =>
        {
            var result = await service.UpdateCategoryProdAsync(request);
            if (result != "OK")
            {
                return Results.NotFound(new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = result
                });
            }

            var response = new ApiResponse<string>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status200OK,
                Message = "Categoría actualizada correctamente."
            };

            return Results.Ok(response);
        })
        .WithName("UpdateCategoryProd")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Purchase");

        return group;
    }
}
