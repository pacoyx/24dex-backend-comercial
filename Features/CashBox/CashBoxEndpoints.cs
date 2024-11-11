public static class CashBoxEndpoints{
    public static void MapCashBox(this WebApplication app){
        var group = app.MapGroup("/api/cashBox");
        group.MapCreateCashBox();
        group.MapCreateCashBoxDetail();
        group.MapGetCashBox();
        group.MapGetCashBoxes();
        group.MapDeleteCashBox();
        group.MapGetCashBoxOpenByUser();
        group.MapCloseCashBox();
        group.MapGetItemsCashBoxDetail();
    }
}