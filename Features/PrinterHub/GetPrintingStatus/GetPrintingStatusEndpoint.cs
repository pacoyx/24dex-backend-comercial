using Microsoft.EntityFrameworkCore;

namespace _24dex_backend_comercial.Features.PrinterHub;

public static class GetPrintingStatusEndpoint
{
    public static void MapGetPrintingStatus(this IEndpointRouteBuilder app)
    {
        app.MapGet("/status", async (RecepcionDbContext db, PrinterRegistry registry) =>
        {
            var totalPendingJobs = await db.PrintQueues
                .CountAsync(x => x.Status == PrintStatus.Pending);

            var errorJobs = await db.PrintQueues
                .CountAsync(x => x.Status == PrintStatus.Error);

            var pendingJobsByPrinter = await db.PrintQueues
                .Where(x => x.Status == PrintStatus.Pending)
                .GroupBy(x => x.PrinterId)
                .Select(g => new { PrinterId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.PrinterId, x => x.Count);

            var printers = await db.Printers
                .AsNoTracking()
                .Include(x => x.WorkerNode)
                    .ThenInclude(x => x.Branch)
                .Select(x => new
                {
                    x.Id,
                    x.WindowsPrinterName,
                    x.WorkerNodeId,
                    ComputerName = x.WorkerNode.ComputerName,
                    BranchId = x.WorkerNode.BranchId,
                    BranchName = x.WorkerNode.Branch.Description
                })
                .ToListAsync();

            var connectedWorkerNodes = registry.GetAll();

            var printerStatuses = printers.Select(printer =>
            {
                var connected = connectedWorkerNodes
                    .FirstOrDefault(x => x.WorkerNodeId == printer.WorkerNodeId);

                return new PrinterStatusResponse(
                    printer.Id,
                    printer.WindowsPrinterName,
                    printer.WorkerNodeId,
                    printer.ComputerName,
                    printer.BranchId,
                    printer.BranchName,
                    connected != null,
                    connected?.ConnectedAt,
                    connected?.LastHeartbeat,
                    pendingJobsByPrinter.GetValueOrDefault(printer.Id)
                );
            }).ToList();

            var response = new PrintingStatusResponse(
                printers.Count,
                registry.Count,
                printerStatuses.Count(x => !x.IsOnline),
                totalPendingJobs,
                errorJobs,
                printerStatuses
            );

            return Results.Ok(response);
        });
    }
}
