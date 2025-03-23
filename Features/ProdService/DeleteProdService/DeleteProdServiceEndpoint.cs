public static class DeleteProdServiceEndpoint
{
    public static RouteHandlerBuilder MapDeleteProdService(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
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

             context.ProdServices.Remove(prodService);
             await context.SaveChangesAsync();

             var response = new ApiResponse<string>
             {
                 Success = true,
                 Data = "ProdService deleted successfully",
                 StatusCode = StatusCodes.Status200OK,
                 Message = "ProdService deleted successfully"
             };

             return Results.Ok(response);
         });
    }
}