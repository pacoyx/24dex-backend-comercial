public class SneakerReportFilter
{
    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public int? BranchId { get; set; }

    public int? BrandId { get; set; }

    public string? TicketStatus { get; set; }

    public bool? PendingDelivery { get; set; }
}

public record SneakerReportRowResponse(
    string WorkGuideNumber,
    string CustomerName,
    string? Brand,
    string? PrimaryColor,
    bool HasLaces,
    bool HasInsoles,
    string DeliveryStatus,
    int PhotoCount
);
