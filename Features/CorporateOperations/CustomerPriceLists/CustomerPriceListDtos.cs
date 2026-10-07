public record CustomerPriceListDetailRequest(int ServiceItemId, decimal UnitPrice);

public record CustomerPriceListDetailResponse(long Id, int ServiceItemId, string ServiceItemName, decimal UnitPrice);

public record CustomerPriceListResponse(
    long Id,
    int CustomerId,
    string Name,
    int Version,
    DateTime EffectiveFrom,
    DateTime? EffectiveTo,
    bool IsActive,
    List<CustomerPriceListDetailResponse> Details
);

public record CustomerPriceListSummaryResponse(
    long Id,
    int CustomerId,
    string Name,
    int Version,
    DateTime EffectiveFrom,
    DateTime? EffectiveTo,
    bool IsActive
);
