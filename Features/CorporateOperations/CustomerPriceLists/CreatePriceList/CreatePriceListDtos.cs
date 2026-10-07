public record CreatePriceListRequest(
    string Name,
    DateTime EffectiveFrom,
    DateTime? EffectiveTo,
    bool Activate,
    List<CustomerPriceListDetailRequest> Details
);
