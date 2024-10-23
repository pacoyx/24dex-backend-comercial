public static class DeleteNumbersDocumentEndpoints
{
    public static void MapDeleteNumbersDocument(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (RecepcionDbContext db, int id) =>
        {
            var numbersDocument = await db.NumbersDocuments.FindAsync(Convert.ToInt32(id));
            if (numbersDocument == null)
            {
                return Results.NotFound();
            }
            db.NumbersDocuments.Remove(numbersDocument);
            await db.SaveChangesAsync();
            return Results.Ok(numbersDocument);
        });
    }
}