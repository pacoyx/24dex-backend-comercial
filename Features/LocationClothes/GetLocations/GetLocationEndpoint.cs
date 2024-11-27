using Microsoft.EntityFrameworkCore;

public static class GetLocation
{
    public static void MapGetLocations(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var location = await context.LocationClothes
            .AsNoTracking()
            .Select(lc => new LocationClothesResponseDto
            (
                 lc.Id,
                 lc.Name,
                 lc.Description
            ))
            .ToListAsync();

            var response = new ApiResponse<List<LocationClothesResponseDto>>
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