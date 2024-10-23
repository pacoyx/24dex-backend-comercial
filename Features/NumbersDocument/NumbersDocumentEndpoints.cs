public static class NumbersDocumentEndpoints{
    public static void MapNumbersDocument(this WebApplication app){
        var group = app.MapGroup("/api/numbersDocument");
        group.MapGetNumbersDocument();
        group.MapGetNumbersDocuments();
        group.MapCreateNumbersDocument();
        group.MapUpdateNumbersDocument();
        group.MapDeleteNumbersDocument();
        group.MapGetNumbersDocumentSearch();
    }
}