public record PrinterComboResponse(
    int Id,
    int WorkerNodeId,
    string WindowsPrinterName,
    bool IsActive,
    DateTime? LastDiscoveredAt
);
