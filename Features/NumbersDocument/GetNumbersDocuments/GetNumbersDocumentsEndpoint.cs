using Microsoft.EntityFrameworkCore;

public static class GetNumbersDocumentsEndpoint{
    public static void MapGetNumbersDocuments(this IEndpointRouteBuilder app){
        app.MapGet("/", async (RecepcionDbContext db) =>{
            var numbersDocuments = await db.NumbersDocuments.ToListAsync();
            return Results.Ok(numbersDocuments);
        });
    }
}