public static class UpdateProdServiceEndpoint
{
    public static void MapUpdateProdService(this IEndpointRouteBuilder app)
    {
        app.MapPut("/", async (RecepcionDbContext context, UpdateProdServiceDto updateProdServiceDto) =>
        {
            var prodService = await context.ProdServices.FindAsync(updateProdServiceDto.Id);
            if (prodService == null)
            {
                return Results.NotFound();
            }
            prodService.Name = updateProdServiceDto.Name;
            prodService.Description = updateProdServiceDto.Description;
            prodService.Price = updateProdServiceDto.Price;
            prodService.Status = updateProdServiceDto.Status;
            prodService.CatServiceId = updateProdServiceDto.CatServiceId;
            context.ProdServices.Update(prodService);
            await context.SaveChangesAsync();
            return Results.Ok(prodService);

        });
    }
}