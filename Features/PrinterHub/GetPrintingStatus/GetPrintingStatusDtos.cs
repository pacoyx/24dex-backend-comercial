namespace _24dex_backend_comercial.Features.PrinterHub;

public record PrinterStatusResponse(
    int PrinterId,
    string WindowsPrinterName,
    int WorkerNodeId,
    string ComputerName,
    int BranchId,
    string BranchName,
    bool IsOnline,
    DateTime? ConnectedAt,
    DateTime? LastHeartbeat,
    int PendingJobs
);

public record PrintingStatusResponse(
    int TotalPrinters,
    int TotalConnectedWorkers,
    int OfflinePrinters,
    int TotalPendingJobs,
    int ErrorJobs,
    List<PrinterStatusResponse> Printers
);
