public static class VehiclesEndpoints
{
    public static void MapVehicles(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vehicles")
            .WithTags("Vehicles")
            .RequirePermission(PermissionCodes.CorporateOperations);

        group.MapCreateVehicle();
        group.MapGetVehicles();
        group.MapGetVehicle();
        group.MapUpdateVehicle();
        group.MapDeleteVehicle();
    }

    internal static VehicleResponse ToResponse(Vehicle vehicle) => new(
        vehicle.Id, vehicle.Plate, vehicle.Description, vehicle.IsActive
    );
}
