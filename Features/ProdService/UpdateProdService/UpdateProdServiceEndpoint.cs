public static class UpdateProdServiceEndpoint
{
    public static RouteHandlerBuilder  MapUpdateProdService(this IEndpointRouteBuilder app)
    {
       return app.MapPut("/{id}", async (int id, UpdateProdServiceDto updateProdServiceDto, RecepcionDbContext context ) =>
        {
            var prodService = await context.ProdServices.FindAsync(id);
            if (prodService == null)
            {
                var responseValidation = new ApiResponse<string>
                {
                    Success = false,
                    Data = "ProdService not found",
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "ProdService not found"
                };

                return Results.NotFound(responseValidation);
            }

            prodService.Name = updateProdServiceDto.Name;
            prodService.Description = updateProdServiceDto.Description;
            prodService.Price = updateProdServiceDto.Price;
            prodService.Status = updateProdServiceDto.Status;
            prodService.CatServiceId = updateProdServiceDto.CatServiceId;
            prodService.IsPeso = updateProdServiceDto.IsPeso;
            prodService.IsLavado = updateProdServiceDto.IsLavado;

            context.ProdServices.Update(prodService);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Success = true,
                Data = "ProdService updated successfully",
                StatusCode = StatusCodes.Status200OK,
                Message = "ProdService updated successfully"
            };

            return Results.Ok(response);

        });
    }
}