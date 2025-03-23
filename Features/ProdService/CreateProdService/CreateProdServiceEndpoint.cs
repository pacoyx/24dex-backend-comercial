using DexterCompany.Models;

public static class CreateProdServiceEndpoint
{
    public static RouteHandlerBuilder MapCreateProdService(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/", async (CreateProdServiceDto createProdServiceDto, RecepcionDbContext context) =>
         {
             if (createProdServiceDto == null)
             {
                 var responseValidation = new ApiResponse<string>
                 {
                     Success = false,
                     Data = "Data is null",
                     StatusCode = StatusCodes.Status400BadRequest,
                     Message = "Data is null"
                 };
                 return Results.BadRequest(responseValidation);
             }

             var prodService = new ProdService
             {
                 Name = createProdServiceDto.Name,
                 Description = createProdServiceDto.Description,
                 Price = createProdServiceDto.Price,
                 Status = createProdServiceDto.Status,
                 CatServiceId = createProdServiceDto.CatServiceId,
                 IsPeso = createProdServiceDto.IsPeso,
                 IsLavado = createProdServiceDto.IsLavado
             };

             await context.ProdServices.AddAsync(prodService);
             await context.SaveChangesAsync();

             var response = new ApiResponse<string>
             {
                 Success = true,
                 Data = "ProdService created successfully",
                 StatusCode = StatusCodes.Status201Created,
                 Message = "ProdService created successfully"
             };


             return Results.Ok(response);

         });
    }
}