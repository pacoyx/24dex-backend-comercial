using FluentValidation;
using Microsoft.EntityFrameworkCore;

public static class CreateVehicleEndpoint
{
    public static void MapCreateVehicle(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            CreateVehicleRequest request,
            IValidator<CreateVehicleRequest> validator,
            RecepcionDbContext db) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var plateInUse = await db.Vehicles.AnyAsync(x => x.Plate == request.Plate);
            if (plateInUse)
            {
                return Results.BadRequest(new[] { "Ya existe un vehículo registrado con esa placa." });
            }

            var vehicle = new Vehicle
            {
                Plate = request.Plate,
                Description = request.Description,
                IsActive = true
            };

            await db.Vehicles.AddAsync(vehicle);
            await db.SaveChangesAsync();

            var response = new ApiResponse<VehicleResponse>
            {
                Success = true,
                Message = "Vehículo registrado correctamente.",
                StatusCode = StatusCodes.Status201Created,
                Data = VehiclesEndpoints.ToResponse(vehicle)
            };

            return Results.Created($"/api/vehicles/{vehicle.Id}", response);
        })
        .WithName("CreateVehicle")
        .Produces<ApiResponse<VehicleResponse>>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
