public class Printer
{
    public int Id { get; set; }

    public int WorkerNodeId { get; set; }

    public string WindowsPrinterName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastDiscoveredAt { get; set; }
    public WorkerNode WorkerNode { get; set; } = null!;
}