namespace _24dex_backend_comercial.Features.PrinterHub;

public class PrintNotificationDto
{
    public long QueueId { get; set; }
    public int PrinterId { get; set; }
    public string? PrinterName { get; set; }
}