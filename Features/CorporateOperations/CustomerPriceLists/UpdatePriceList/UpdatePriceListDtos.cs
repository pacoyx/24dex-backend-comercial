public record UpdatePriceListRequest(
    string Name,
    DateTime EffectiveFrom,
    DateTime? EffectiveTo,
    List<CustomerPriceListDetailRequest> Details
);
