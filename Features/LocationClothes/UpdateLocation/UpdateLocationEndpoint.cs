using Microsoft.EntityFrameworkCore;

public static class UpdateLocationEndpoint{
    public static void MapUpdateLocation(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (RecepcionDbContext context, int id, UpdateLocationRequestDto request) =>
        {
            var location = await context.LocationClothes
            .FirstOrDefaultAsync(lc => lc.Id == id);

            if (location == null)
            {
                return Results.NotFound();
            }

            location.Name = request.Nombre;
            location.Description = request.Descripcion;

            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Location updated",
                Message = "Location updated",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}