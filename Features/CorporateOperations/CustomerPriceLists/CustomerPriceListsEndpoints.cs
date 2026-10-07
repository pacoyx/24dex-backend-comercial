public static class CustomerPriceListsEndpoints
{
    public static void MapCustomerPriceLists(this IEndpointRouteBuilder app)
    {
        var customerGroup = app.MapGroup("/api/customers")
            .WithTags("CustomerPriceLists")
            .RequirePermission(PermissionCodes.CorporateOperations);

        customerGroup.MapGetPriceLists();
        customerGroup.MapGetActivePriceList();
        customerGroup.MapCreatePriceList();

        var priceListGroup = app.MapGroup("/api/price-lists")
            .WithTags("CustomerPriceLists")
            .RequirePermission(PermissionCodes.CorporateOperations);

        priceListGroup.MapGetPriceList();
        priceListGroup.MapUpdatePriceList();
        priceListGroup.MapClonePriceList();
        priceListGroup.MapActivatePriceList();
        priceListGroup.MapDeactivatePriceList();
    }

    internal static CustomerPriceListResponse ToResponse(CustomerPriceList priceList) => new(
        priceList.Id,
        priceList.CustomerId,
        priceList.Name,
        priceList.Version,
        priceList.EffectiveFrom,
        priceList.EffectiveTo,
        priceList.IsActive,
        priceList.Details.Select(d => new CustomerPriceListDetailResponse(
            d.Id, d.ServiceItemId, d.ServiceItem.Name, d.UnitPrice
        )).ToList()
    );

    internal static CustomerPriceListSummaryResponse ToSummaryResponse(CustomerPriceList priceList) => new(
        priceList.Id,
        priceList.CustomerId,
        priceList.Name,
        priceList.Version,
        priceList.EffectiveFrom,
        priceList.EffectiveTo,
        priceList.IsActive
    );
}
