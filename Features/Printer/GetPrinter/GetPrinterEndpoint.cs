public static class GetPrinterEndpoint
{
    public static void MapGetPrinter(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext context) =>
        {
            var printer = await context.Printers.FindAsync(id);
            if (printer == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(printer);
        });
    }
}
