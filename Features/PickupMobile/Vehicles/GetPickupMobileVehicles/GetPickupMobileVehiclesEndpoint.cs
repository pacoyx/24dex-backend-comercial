using Microsoft.EntityFrameworkCore;

public static class GetPickupMobileVehiclesEndpoint
{
    public static void MapGetPickupMobileVehicles(this IEndpointRouteBuilder app)
    {
        app.MapGet("/vehicles", async (RecepcionDbContext db) =>
        {
            var vehicles = await db.Vehicles
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Plate)
                .Select(x => new PickupMobileVehicleLookupDto(x.Id, x.Plate, x.Description))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<PickupMobileVehicleLookupDto>>
            {
                Success = true,
                Message = "Vehículos disponibles.",
                StatusCode = StatusCodes.Status200OK,
                Data = vehicles
            });
        })
        .WithName("GetPickupMobileVehicles")
        .Produces<ApiResponse<List<PickupMobileVehicleLookupDto>>>(StatusCodes.Status200OK);
    }
}
