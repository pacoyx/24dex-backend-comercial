using Microsoft.EntityFrameworkCore;

public static class GetVehiclesEndpoint
{
    public static void MapGetVehicles(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (bool? includeInactive, RecepcionDbContext db) =>
        {
            var query = db.Vehicles.AsNoTracking().AsQueryable();
            if (includeInactive != true)
            {
                query = query.Where(x => x.IsActive);
            }

            var vehicles = await query.OrderBy(x => x.Plate).ToListAsync();

            return Results.Ok(new ApiResponse<List<VehicleResponse>>
            {
                Success = true,
                Message = "Listado de vehículos.",
                StatusCode = StatusCodes.Status200OK,
                Data = vehicles.Select(VehiclesEndpoints.ToResponse).ToList()
            });
        })
        .WithName("GetVehicles")
        .Produces<ApiResponse<List<VehicleResponse>>>(StatusCodes.Status200OK);
    }

    public static void MapGetVehicle(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", async (int id, RecepcionDbContext db) =>
        {
            var vehicle = await db.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle is null)
            {
                return Results.NotFound("Vehículo no encontrado.");
            }

            return Results.Ok(new ApiResponse<VehicleResponse>
            {
                Success = true,
                Message = "Vehículo obtenido correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = VehiclesEndpoints.ToResponse(vehicle)
            });
        })
        .WithName("GetVehicle")
        .Produces<ApiResponse<VehicleResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
