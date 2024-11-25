public static class  CreateLocationEndpoint{
    public static void MapCreateLocation(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateLocationDto locationDto, RecepcionDbContext context) =>
        {
            if (locationDto == null)
            {
                return Results.BadRequest();
            }

            var location = new LocationClothes()
            {
                Name = locationDto.Nombre,
                Description = locationDto.Descripcion                
            };

            await context.LocationClothes.AddAsync(location);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>{
                Data = location.Id.ToString(),
                Message = "Location created",
                StatusCode = StatusCodes.Status201Created,
                Success = true
            };
            
            return Results.Ok(response);            
        });
    }
}