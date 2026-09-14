using Microsoft.EntityFrameworkCore;

public static class UpdatePrinterEndpoint
{
    public static void MapUpdatePrinter(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (RecepcionDbContext context, int id, UpdatePrinterDto request) =>
        {
            var printer = await context.Printers
            .FirstOrDefaultAsync(p => p.Id == id);

            if (printer == null)
            {
                return Results.NotFound();
            }

            var workerNode = await context.WorkerNodes.FirstOrDefaultAsync(wn => wn.Id == request.WorkerNodeId);
            if (workerNode == null)
            {
                return Results.BadRequest("No se encontró el worker node");
            }

            printer.WorkerNodeId = request.WorkerNodeId;
            printer.WindowsPrinterName = request.WindowsPrinterName;

            await context.SaveChangesAsync();

            var response = new ApiResponse<string>
            {
                Data = "Impresora actualizada",
                Message = "Impresora actualizada",
                StatusCode = StatusCodes.Status200OK,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
