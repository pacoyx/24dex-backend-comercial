public static class PositionEndpoints
{
    public static RouteGroupBuilder MapPosition(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/hr/positions").WithTags("HR - Positions");

        group.MapCreatePosition();
        group.MapGetPositions();
        group.MapUpdatePosition();
        group.MapDeletePosition();

        return group;
    }
}
