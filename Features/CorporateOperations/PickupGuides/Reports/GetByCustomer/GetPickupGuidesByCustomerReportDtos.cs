public record PickupGuideByCustomerReportItem(
    int CustomerId,
    string CustomerName,
    int GuideCount,
    decimal TotalWeight,
    decimal TotalAmount
);
