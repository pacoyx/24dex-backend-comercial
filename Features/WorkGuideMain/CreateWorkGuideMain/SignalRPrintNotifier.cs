using _24dex_backend_comercial.Features.PrinterHub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace _24dex_backend_comercial.Features.WorkGuideMain.CreateWorkGuideMain;

public sealed class SignalRPrintNotifier : IPrintNotifier
{
    private readonly IHubContext<PrintHub> _hubContext;
    private readonly RecepcionDbContext _db;

    public SignalRPrintNotifier(IHubContext<PrintHub> hubContext, RecepcionDbContext db)
    {
        _hubContext = hubContext;
        _db = db;
    }

    public async Task NotifyAsync(int printerId, long printQueueId)
    {
        var printer = await _db.Printers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == printerId);

        if (printer == null)
        {
            return;
        }

        await _hubContext.Clients
            .Group($"worker-{printer.WorkerNodeId}")
            .SendAsync(
                "PrintAvailable",
                new PrintNotificationDto
                {
                    QueueId = printQueueId,
                    PrinterId = printerId,
                    PrinterName = printer.WindowsPrinterName
                });
    }
}