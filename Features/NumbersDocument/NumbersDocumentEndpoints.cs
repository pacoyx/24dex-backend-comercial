public static class NumbersDocumentEndpoints{
    public static void MapNumbersDocument(this IEndpointRouteBuilder app){
        var group = app.MapGroup("/api/numbersDocument").RequirePermission(PermissionCodes.Settings);
        group.MapGetNumbersDocument();
        group.MapGetNumbersDocuments();
        group.MapCreateNumbersDocument();
        group.MapUpdateNumbersDocument();
        group.MapDeleteNumbersDocument();
        group.MapGetNumbersDocumentSearch();
    }
}