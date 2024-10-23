public static class UpdateNumbersDocument
{
    public static void MapUpdateNumbersDocument(this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (RecepcionDbContext db, int id, UpdateNumbersDocumentDto updateNumbersDocumentDto) =>
        {
            if (updateNumbersDocumentDto == null){
                return Results.BadRequest("Update data is missing");
            }
            if (id != updateNumbersDocumentDto.Id){
                return Results.BadRequest("Id mismatch");
            }

            var numbersDocument = await db.NumbersDocuments.FindAsync(Convert.ToInt32(id));
            if (numbersDocument == null)
            {
                return Results.NotFound();
            }
            numbersDocument.NumberDoc = updateNumbersDocumentDto.NumberDoc;
            numbersDocument.BranchId = updateNumbersDocumentDto.BranchId;
            numbersDocument.TypeDoc = updateNumbersDocumentDto.TypeDoc;
            numbersDocument.SerieDoc = updateNumbersDocumentDto.SerieDoc;
            numbersDocument.Status = updateNumbersDocumentDto.Status;
            await db.SaveChangesAsync();
            return Results.Ok(numbersDocument);
        });
    }
}