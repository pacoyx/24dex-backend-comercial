using DexterCompany.Models;

public static class CreateCatServiceEndpoints
{
    public static void MapCreateCatService(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (RecepcionDbContext context, CreateCatServiceDto createCatServiceDto) =>
        {
            if (createCatServiceDto == null)
            {
                return Results.BadRequest();
            }

            var catService = new CatService()
            {
                Name = createCatServiceDto.Name,
                Description = createCatServiceDto.Description,
                Icon = createCatServiceDto.Icon
            };

            await context.CatServices.AddAsync(catService);
            await context.SaveChangesAsync();
            return Results.CreatedAtRoute($"/api/catServices/{catService.Id}", catService);
        });
    }
}