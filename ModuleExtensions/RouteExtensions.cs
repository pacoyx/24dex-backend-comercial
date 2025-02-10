using Microsoft.AspNetCore.Builder;

public static class RouteExtensions
{
    public static void MapRoutes(this IEndpointRouteBuilder app)
    {
        app.MapLogin();
        app.MapExpenseBox();
        app.MapHealth();
        app.MapBranchSales();
        app.MapCustomer();
        app.MapCatService();
        app.MapProdService();
        app.MapNumbersDocument();
        app.MapWorkShift();
        app.MapWorkGuideMain();
        app.MapCashBox();
        app.MapReport();
        app.MapLocationClothes();
        app.MapUser();
        app.MapCompany();
        app.MapTicket();
        app.MapCollectionGuide();
        app.MapClothingWorkerEndpoints();
        app.MapClothingItemEndpoints();
    }
}