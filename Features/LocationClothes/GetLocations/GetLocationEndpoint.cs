using Microsoft.EntityFrameworkCore;

public static class GetLocation
{
    public static void MapGetLocations(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var location = await context.LocationClothes
            .AsNoTracking()
            .ToListAsync();

            var response = new ApiResponse<List<LocationClothes>>
            {
                Data = location,
                Message = "Location found",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}