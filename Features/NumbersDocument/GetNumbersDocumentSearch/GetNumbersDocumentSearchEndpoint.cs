using Microsoft.EntityFrameworkCore;

public static class GetNumbersDocumentSearchEndpoints
{
    public static void MapGetNumbersDocumentSearch(this IEndpointRouteBuilder app)
    {
        app.MapGet("/search", async (int branchId, string typeDoc, string serieDoc, RecepcionDbContext db) =>
        {
            var numbersDocument = await db.NumbersDocuments
                .FirstOrDefaultAsync(nd => nd.BranchId == branchId && nd.TypeDoc == typeDoc && nd.SerieDoc == serieDoc);
            if (numbersDocument == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(numbersDocument);
        });
    }
}