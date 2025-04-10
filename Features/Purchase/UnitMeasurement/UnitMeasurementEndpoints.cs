public static class UnitMeasurementEndpoints
{

    public static RouteGroupBuilder MapUnitMeasurement(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/unitMeasurement");

        group.MapCreateUnitMeasurement();
        group.MapGetUnitMeasurements();
        group.MapGetUnitMeasurementsShort();
        // group.MapGetUnitMeasurement();
        // group.MapDeleteUnitMeasurement();
        // group.MapUpdateUnitMeasurement();
        // group.MapGetUnitMeasurementByCat();
        // group.MapGetUnitMeasurementSearchByDescription();

        return group;
    }

}