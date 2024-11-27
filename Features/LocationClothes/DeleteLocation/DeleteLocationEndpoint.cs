using Microsoft.EntityFrameworkCore;

public static class DeleteLocationEndpoint{
    public static void MapDeleteLocation(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var location = await context.LocationClothes
            .FirstOrDefaultAsync(lc => lc.Id == id);

            if (location == null)
            {
                return Results.NotFound();
            }

            context.LocationClothes.Remove(location);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Location deleted",
                Message = "Location deleted",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}