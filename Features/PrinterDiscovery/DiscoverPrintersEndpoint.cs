using Microsoft.EntityFrameworkCore;

public static class DiscoverPrintersEndpoint
{
    public static void MapDiscoverPrinters(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/api/workers/discovery",
            async (
                PrinterDiscoveryRequest request,
                RecepcionDbContext db,
                ILogger<Program> logger) =>
            {
                var workerNode =
                    await db.WorkerNodes
                        .SingleOrDefaultAsync(x =>
                            x.WorkerKey ==
                            request.WorkerKey);

                if (workerNode is null)
                {
                    return Results.BadRequest(
                        $"WorkerKey '{request.WorkerKey}' no registrado");
                }

                workerNode.ComputerName =
                    request.ComputerName;

                workerNode.LastConnectionAt =
                    DateTime.UtcNow;

                foreach (var printerName in request.Printers)
                {
                    var printer =
                        await db.Printers
                            .SingleOrDefaultAsync(x =>
                                x.WorkerNodeId ==
                                    workerNode.Id &&
                                x.WindowsPrinterName ==
                                    printerName);

                    if (printer is null)
                    {
                        db.Printers.Add(
                            new Printer
                            {
                                WorkerNodeId =
                                    workerNode.Id,

                                WindowsPrinterName =
                                    printerName,

                                CreatedAt =
                                    DateTime.UtcNow,

                                LastDiscoveredAt =
                                    DateTime.UtcNow,

                                IsActive = true
                            });

                        logger.LogInformation(
                            "Nueva impresora detectada: {Printer}",
                            printerName);
                    }
                    else
                    {
                        printer.LastDiscoveredAt =
                            DateTime.UtcNow;

                        printer.IsActive = true;
                    }
                }

                await db.SaveChangesAsync();

                return Results.Ok(new
                {
                    WorkerNodeId = workerNode.Id,
                    PrinterCount = request.Printers.Count
                });
            });
    }
}
