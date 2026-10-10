public record PortalServiceConsumptionItem(
    int ServiceItemId,
    string ServiceItemName,
    int GuideCount,
    decimal TotalQuantity,
    decimal TotalWeight,
    decimal TotalAmount
);
