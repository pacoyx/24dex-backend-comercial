public static class DeleteCategoryProdEndpoint
{
    public static RouteGroupBuilder MapDeleteCategoryProd(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (int id, IDeleteCategoryProdService service) =>
        {
            var result = await service.DeleteCategoryProdAsync(id);
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
                Message = "Categoría desactivada correctamente."
            };

            return Results.Ok(response);
        })
        .WithName("DeleteCategoryProd")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Purchase");

        return group;
    }
}
