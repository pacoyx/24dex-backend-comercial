using DexterCompany.Models;

public static class CreateProdServiceEndpoint{
    public static void MapCreateProdService(this IEndpointRouteBuilder app){
        app.MapPost("/", async (CreateProdServiceDto createProdServiceDto, RecepcionDbContext context) =>{
            if(createProdServiceDto == null){
                return Results.BadRequest("Data is null");
            }

            var prodService = new ProdService{
                Name = createProdServiceDto.Name,
                Description = createProdServiceDto.Description,
                Price = createProdServiceDto.Price,
                Status = createProdServiceDto.Status,
                CatServiceId = createProdServiceDto.CatServiceId                
            };

            await context.ProdServices.AddAsync(prodService);
            await context.SaveChangesAsync();
            return Results.CreatedAtRoute($"/api/prodServices/{prodService.Id}", prodService);
        });
    }
}