using Microsoft.EntityFrameworkCore;

public static class DeletePrinterEndpoint
{
    public static void MapDeletePrinter(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext context, int id) =>
        {
            var printer = await context.Printers
            .FirstOrDefaultAsync(p => p.Id == id);

            if (printer == null)
            {
                return Results.NotFound();
            }

            context.Printers.Remove(printer);
            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Impresora eliminada",
                Message = "Impresora eliminada",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
