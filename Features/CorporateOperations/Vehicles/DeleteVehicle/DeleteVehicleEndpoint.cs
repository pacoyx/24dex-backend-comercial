using Microsoft.EntityFrameworkCore;

public static class DeleteVehicleEndpoint
{
    public static void MapDeleteVehicle(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id:int}", async (int id, RecepcionDbContext db) =>
        {
            var vehicle = await db.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle is null)
            {
                return Results.NotFound("Vehículo no encontrado.");
            }

            // Baja lógica: PickupGuide puede referenciar el vehículo históricamente.
            vehicle.IsActive = false;
            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Vehículo desactivado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = new { vehicle.Id, vehicle.IsActive }
            });
        })
        .WithName("DeleteVehicle")
        .Produces<ApiResponse<object>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
