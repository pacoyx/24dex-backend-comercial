using Microsoft.EntityFrameworkCore;

public static class GetNumbersDocumentEndpoint
{
    public static void MapGetNumbersDocument(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, RecepcionDbContext db) =>
        {
            if (id <= 0)
            {
                return Results.BadRequest("Id must be greater than 0");
            }
            var numbersDocument = await db.NumbersDocuments.FindAsync(Convert.ToInt32(id));
            return Results.Ok(numbersDocument);
        });
    }
}