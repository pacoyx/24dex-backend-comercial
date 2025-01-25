public static class GetHealthEndpoint
{
    public static void MapGetHealth(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () =>
        {
            return Results.Ok(new { message = "API is running" });
        });
    }
}