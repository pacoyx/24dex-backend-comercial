public sealed class PrinterDiscoveryRequest
{
    public string WorkerKey { get; set; } = string.Empty;

    public string ComputerName { get; set; } = string.Empty;

    public List<string> Printers { get; set; } = [];
}
