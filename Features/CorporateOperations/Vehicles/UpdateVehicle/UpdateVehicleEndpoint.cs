using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class UpdateVehicleEndpoint
{
    public static void MapUpdateVehicle(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:int}", async (
            int id,
            UpdateVehicleRequest request,
            IValidator<UpdateVehicleRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var vehicle = await db.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicle is null)
            {
                return Results.NotFound("Vehículo no encontrado.");
            }

            var plateInUse = await db.Vehicles.AnyAsync(x => x.Id != id && x.Plate == request.Plate);
            if (plateInUse)
            {
                return Results.BadRequest(new[] { "Ya existe un vehículo registrado con esa placa." });
            }

            vehicle.Plate = request.Plate;
            vehicle.Description = request.Description;
            vehicle.IsActive = request.IsActive;

            await db.SaveChangesAsync();

            return Results.Ok(new ApiResponse<VehicleResponse>
            {
                Success = true,
                Message = "Vehículo actualizado correctamente.",
                StatusCode = StatusCodes.Status200OK,
                Data = VehiclesEndpoints.ToResponse(vehicle)
            });
        })
        .WithName("UpdateVehicle")
        .Produces<ApiResponse<VehicleResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
